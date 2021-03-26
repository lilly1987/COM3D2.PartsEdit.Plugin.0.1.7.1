using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace CM3D2.PartsEdit.Plugin
{
	[PluginName("CM3D2 PartsEditPlugin")]
	[PluginVersion("0.1.7.1")]
	internal class PartsEditPlugin : PluginBase
	{
		private void Awake()
		{
			this.SetPluginInfo();
			this.CreatePluginObject();
		}

		private void SetPluginInfo()
		{
			PluginNameAttribute pluginNameAttribute = Attribute.GetCustomAttribute(base.GetType(), typeof(PluginNameAttribute)) as PluginNameAttribute;
			PluginVersionAttribute pluginVersionAttribute = Attribute.GetCustomAttribute(base.GetType(), typeof(PluginVersionAttribute)) as PluginVersionAttribute;
			string name = (pluginNameAttribute == null) ? base.Name : pluginNameAttribute.Name;
			string version = (pluginVersionAttribute == null) ? string.Empty : pluginVersionAttribute.Version;
			string @namespace = base.GetType().Namespace;
			PluginInfo.SetInfo(name, version, @namespace);
			IniUtil.Init(this);
			IniUtil.saveMethod += base.SaveConfig;
			Setting.LoadIni();
			Setting.SaveIni();
		}

		private void CreatePluginObject()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "PartsEdit.Plugin";
			gameObject.AddComponent<PartsEdit>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		public void OnStudioExNotifyInitStart(PluginBase sender)
		{
			this.hPluginStudioExCoPluginManager = sender;
			this.hPluginStudioExCoPluginManager.SendMessage("OnStudioExCallRegistPlugin", PluginInfo.Name);
		}

		public void OnStudioExCallSave(Dictionary<string, XElement[]> hConfig)
		{
			try
			{
				XElement xelement = new XElement("PartsEdit");
				xelement.Add(new XElement("PluginVersion", "1.7"));
				xelement.Add(SceneDataManager.GetSceneXmlData());
				XElement[] value = new XElement[]
				{
					xelement
				};
				KeyValuePair<string, XElement[]> keyValuePair = new KeyValuePair<string, XElement[]>(PluginInfo.Name, value);
				this.hPluginStudioExCoPluginManager.SendMessage("OnStudioExCallWriteXML", keyValuePair);
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}

		public void OnStudioExCallLeave(Dictionary<string, XElement[]> hConfig)
		{
			try
			{
				XElement xelement = new XElement("PartsEdit");
				xelement.Add(new XElement("PluginVersion", "1.7"));
				xelement.Add(SceneDataManager.GetSceneXmlData());
				XElement[] value = new XElement[]
				{
					xelement
				};
				KeyValuePair<string, XElement[]> keyValuePair = new KeyValuePair<string, XElement[]>(PluginInfo.Name, value);
				this.hPluginStudioExCoPluginManager.SendMessage("OnStudioExCallWriteXML", keyValuePair);
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}

		public void OnStudioExCallLoadFinishing(Dictionary<string, XElement[]> hConfig)
		{
			try
			{
				string empty = string.Empty;
				XElement[] array = hConfig.ContainsKey(PluginInfo.Name) ? hConfig[PluginInfo.Name] : null;
				bool flag = array != null;
				if (flag)
				{
					XElement xelement = array[0];
					XElement sceneXmlData = xelement.Element("SceneData");
					SceneDataManager.SetSceneXmlData(sceneXmlData);
				}
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}

		public void OnStudioExCallEnterFinishing(Dictionary<string, XElement[]> hConfig)
		{
			try
			{
				string empty = string.Empty;
				XElement[] array = hConfig.ContainsKey(PluginInfo.Name) ? hConfig[PluginInfo.Name] : null;
				bool flag = array != null;
				if (flag)
				{
					XElement xelement = array[0];
					XElement sceneXmlData = xelement.Element("SceneData");
					SceneDataManager.SetSceneXmlData(sceneXmlData);
				}
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}

		protected PluginBase hPluginStudioExCoPluginManager;
	}
}
