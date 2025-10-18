using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(Item), true)]
[CanEditMultipleObjects]
public class Item_Editor : Editor
{
    private Item Item { get { return target as Item; } }

    // Overrides the scriptableobject static preview icon to display our Item icon instead of the default icon for the class
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        if (Item.Icon != null)
        {
            // Uses reflection to manually invoke a hidden method in the hidden type UnityEditor.SpriteUtility to force render a custom image as the asset preview
            Type spriteUtilityType = GetInternalType("UnityEditor.SpriteUtility");
            if (spriteUtilityType != null)
            {
                MethodInfo renderPreviewMethod = spriteUtilityType.GetMethod("RenderStaticPreview", new Type[] { typeof(Sprite), typeof(Color), typeof(int), typeof(int) });
                if (renderPreviewMethod != null)
                {
                    object customTexture = renderPreviewMethod.Invoke("RenderStaticPreview", new object[] { Item.Icon, Color.white, width, height });
                    if (customTexture is Texture2D)
                        return customTexture as Texture2D;
                }
            }
        }
        return base.RenderStaticPreview(assetPath, subAssets, width, height);
    }
    
    public static Type GetInternalType(string TypeName)
    {
        var type = Type.GetType(TypeName);
        if (type != null)
            return type;

        if (TypeName.Contains("."))
        {
            var assemblyName = TypeName[..TypeName.IndexOf('.')];
            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
                return null;
            type = assembly.GetType(TypeName);
            if (type != null)
                return type;
        }

        var currentAssembly = Assembly.GetExecutingAssembly();
        var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
        foreach (var assemblyName in referencedAssemblies)
        {
            var assembly = Assembly.Load(assemblyName);
            if (assembly != null)
            {
                type = assembly.GetType(TypeName);
                if (type != null)
                    return type;
            }
        }
        return null;
    }
}
