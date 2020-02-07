using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using NLog;
using TomskNipi.NotificationsService.API;

namespace TomskNipi.NotificationsService.Core
{
    /// <summary>
    /// Реализация провайдера для получения списка адресов для скрытой копии
    /// </summary>
    public class HiddenEmailListProviderImplementation : IHiddenEmailListProvider
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Наименование файла конфигурации сервиса со списком адресов для скрытой копии
        /// </summary>
        private const string serviceConfigFileName = "service.config";

        /// Grebenkovma TECHDEBT Копируется из проекта в проект
        /// <summary>
        /// Возвращает путь к папке с бинарниками
        /// </summary>
        private string RootPath
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new Uri(codeBase);
                var path = uri.LocalPath;
                var root = Path.GetDirectoryName(path);
                return root;
            }
        }

        /// <inheritdoc />
        public string[] GetEmailList()
        {
            var resultEmailList = new List<string>();

            var pathToServiceConfigXML = Path.Combine(RootPath, serviceConfigFileName);

            // Grebenkovma TECHDEBT В данном случае выбран не лучший класс для работы с XML, основная проблема в том что нет возможности использовать LINQ и нет возможности сериализовать и десериализовать 
            var hiddenEmailXML = new XmlDocument();

            try
            {
                hiddenEmailXML.Load(pathToServiceConfigXML);
                Logger.Debug("Загрузка файла конфигурации сервиса.");
            }
            catch (FileNotFoundException e)
            {
                Logger.Warn(e, "Загрузка файла конфигурации завершилась исключением.");
                throw new NotificationsServiceException("Загрузка файла конфигурации завершилась исключением.", e);
            }

            var xRoot = hiddenEmailXML.DocumentElement;

            if (xRoot == null) return resultEmailList.ToArray();
                
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Name == "email")
                {
                    resultEmailList.Add(xnode.InnerText);
                }
            }
            
            return resultEmailList.ToArray();
        }
    }
}