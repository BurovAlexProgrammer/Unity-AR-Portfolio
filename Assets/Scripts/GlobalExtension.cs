using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public static class GlobalExtension
{
    /// <summary>
    /// Вывести сообщение в консоль.
    /// </summary>
    /// <param name="message">Сообщение</param>
    public static void Log(string message)
    {
        Debug.Log(message);
    }
    /// <summary>
    /// Вывести ошибку в консоль
    /// </summary>
    /// <param name="message">Сообщение</param>
    public static void Error(string message)
    {
        Debug.LogError(message);
    }

    public static void Error<T>(T ob)
    {
        throw new Exception(ob.ToString());
    }


    /// <summary>
    /// Вернуть первого потомка(Transform) с указанным тегом
    /// </summary>
    /// <param name="tag">Тег</param>
    /// <returns>child Transform</returns>
    public static Transform FindChildByTag(this Scene scene, string tag)
    {
        foreach (var parent in scene.GetRootGameObjects())
        {
            var child = parent.transform.FindChildByTag(tag);
            if (child != null) { return child; }
        }
        return null;
    }

    /// <summary>
    /// Вернуть первого потомка (Transform) с указанным тегом из родителя (Transform)
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns>child Transform</returns>
    public static Transform FindChildByTag(this Transform parent, string tag)
    {
        var children = parent.transform.Children();
        return children.Where(c => c.CompareTag(tag)).DefaultIfEmpty(null).FirstOrDefault();
    }

    /// <summary>
    /// Вернуть список потомков (Transform) из родителя (Transform)
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static List<Transform> ChildrenList(this Transform parent)
    {
        var result = new List<Transform>();
        foreach (Transform transform in parent)
        {
            result.Add(transform);
            if (transform.childCount > 0)
                result.AddRange(transform.ChildrenList());
        }
        return result;
    }

    /// <summary>
    /// Вернуть потомков GameObject как IEnumerable из иерархии родителя
    /// </summary>
    /// <param name="parent"Родитель(Transform)></param>
    /// <param name="tag">Тег</param>
    /// <returns></returns>
    public static IEnumerable<Transform> Children(this Transform parent)
    {
        foreach (Transform transform in parent)
        {
            yield return transform;
            if (transform.childCount > 0)
                foreach (var item in transform.Children())
                    yield return item;
        }
    }

    /// <summary>
    /// Вернуть потомков (Transform) с тегом. 
    /// </summary>
    /// <param name="parent">Родитель(GameObject)</param>
    /// <param name="tag">Тег</param>
    /// <param name="onlyActive">Берет только активных</param>
    /// <returns>List(Transform)</returns>
    public static List<Transform> FindChildrenByTag(this GameObject parent, string tag, bool onlyActive = false)
    {
        var children = parent.transform.Children();
        if (onlyActive)
            return children.Where(c => c.CompareTag(tag) & c.gameObject.activeSelf).ToList();
        else
            return children.Where(c => c.CompareTag(tag)).ToList();
    }

    /// <summary>
    /// Проверяет на (null or destroyed) текущий объект и возвращает bool.
    /// </summary>
    /// <param name="obj">Проверяемый объект</param>
    /// <returns>bool</returns>
    public static bool NotExist(this object obj)
    {
        var result = (obj as IComponent)?.isDestroyed ?? true;
        return result;
    }

    /// <summary>
    /// Проверяет на (null or destroyed) текущий объект и возвращает bool.
    /// </summary>
    /// <param name="obj">Проверяемый объект</param>
    /// <returns>bool</returns>
    public static bool Exist(this object obj)
    {
        return !obj.NotExist();
    }

    /// <summary>
    /// Проверяет на (null or destroyed) текущий объект и выводит ошибку в консоль.
    /// </summary>
    /// <param name="obj">Проверяемый объект</param>
    public static void CheckExist(this GameObject obj)
    {
        if (obj.NotExist()) Error(obj);
    }

    /// <summary>
    /// Возвращает родителя (GameObject) с тегом.
    /// </summary>
    /// <param name="childObject"></param>
    /// <param name="tag"></param>
    /// <returns>родитель (GameObject) или null при неудаче.</returns>
    public static GameObject FindParentByTag(this GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null;
    }

    interface IComponent
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        Component component { get; }
        bool isDestroyed { get; }
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public static void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif
    }

    /// <summary>
    /// Установка угла Ейлера по оси Y
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void SetEulerY(this Transform transform, float y)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }

}
