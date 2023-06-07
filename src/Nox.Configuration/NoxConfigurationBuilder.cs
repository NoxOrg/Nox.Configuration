using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Nox.Exceptions;
using Nox.Utilities.Yaml;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Nox
{
    public class NoxConfigurationBuilder
    {
        private const string DesignFolderBestPractice = "Best practice is to create a '.nox' folder in your solution folder and in there a 'design' folder which contains your <solution-name>.solution.nox.yaml";

        private string _yamlFilePath = string.Empty;
        private bool _mustInject = false;
        private IServiceCollection? _services;

        public NoxConfigurationBuilder UseYamlFile(string yamlFilePath)
        {
            _yamlFilePath = Path.GetFullPath(yamlFilePath);
            return this;
        }

        public NoxConfigurationBuilder UseDependencyInjection(IServiceCollection services)
        {
            _mustInject = true;
            _services = services;
            return this;
        }
        
        public NoxConfiguration Build()
        {
            //If a yaml root configuration has not been specified, search for one in the .nox/design folder in the solution root folder
            if (string.IsNullOrWhiteSpace(_yamlFilePath))
            {
                _yamlFilePath = Path.GetFullPath(FindRootYamlFile());
            }
            else
            {
                //Ensure that the root yaml file exists
                if (!File.Exists(_yamlFilePath))
                {
                    throw new NoxConfigurationException($"Nox root yaml configuration file ({_yamlFilePath}) not found! Have you created a Nox yaml configuration for your solution? {DesignFolderBestPractice}");
                }    
            }

            var config = new NoxConfiguration(_yamlFilePath);
            config.Validate();

            if (_mustInject)
            {
                _services.AddSingleton(config);
            }
            
            return config;
        }

        private string? FindSolutionRoot()
        {
            var path = new DirectoryInfo( Directory.GetCurrentDirectory() );

            while (path != null)
            {
                if (path.GetDirectories(".git").Any())
                {
                    return path.FullName;
                }
                path = path.Parent;
            }

            return null;
        }

        private string? FindNoxDesignFolder(string rootPath)
        {
            var path = new DirectoryInfo(rootPath);
            if (path.GetDirectories(".nox").Any())
            {
                path = new DirectoryInfo(Path.Combine(path.FullName, ".nox"));
                if (path.GetDirectories("design").Any())
                {
                    return Path.Combine(path.FullName, "design");
                }
            }
            return null;
        }

        private string FindRootYamlFile()
        {
            var solutionRoot = FindSolutionRoot();
            if (string.IsNullOrWhiteSpace(solutionRoot))
            {
                throw new NoxConfigurationException("Unable to locate the root folder of your solution. Have you created a git repo for your solution?");
            }

            var designFolder = FindNoxDesignFolder(solutionRoot!);
            if (string.IsNullOrWhiteSpace(designFolder))
            {
                throw new NoxConfigurationException($"Unable to locate a .nox/design folder in your solution folder ({solutionRoot}). Best practice is to create a '.nox' folder in your solution folder and in there a 'design' folder which contains your <solution-name>.solution.nox.yaml and supporting files.");
            }

            var solutionYamlFiles = Directory.GetFiles(designFolder, "*.solution.nox.yaml");
            if (solutionYamlFiles.Length == 0)
            {
                throw new NoxConfigurationException($"Could not find a *.solution.nox.yaml file in your nox design folder ({designFolder}). {DesignFolderBestPractice}");
            } else if (solutionYamlFiles.Length > 1)
            {
                throw new NoxConfigurationException($"Found more than one *.solution.nox.yaml file in your nox design folder ({designFolder}). {DesignFolderBestPractice}");
            }

            return solutionYamlFiles[0];
        }
    }
}