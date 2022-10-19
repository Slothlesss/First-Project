using System;
using System.Collections;

namespace Assets.HeroEditor4D.Common.CommonScripts
{
	public static class StandaloneFilePicker
	{
		#if UNITY_EDITOR

		public static IEnumerator OpenFile(string title, string directory, string extension, Action<bool, string, byte[]> callback)
		{
			var path = UnityEditor.EditorUtility.OpenFilePanel(title, directory, extension);

			if (!string.IsNullOrEmpty(path))
			{
				var bytes = System.IO.File.ReadAllBytes(path);

				callback(true, path, bytes);
			}
			else
			{
				callback(false, null, null);
			}

			yield break;
		}

		public static IEnumerator SaveFile(string title, string directory, string defaultName, string extension, byte[] bytes, Action<bool, string> callback)
		{
			var path = UnityEditor.EditorUtility.SaveFilePanel(title, directory, defaultName, extension);
			

			if (!string.IsNullOrEmpty(path))
			{
				System.IO.File.WriteAllBytes(path, bytes);
				callback(true, path);
			}
			else
			{
				callback(false, null);
			}

			yield break;
		}

		#endif
	}
}