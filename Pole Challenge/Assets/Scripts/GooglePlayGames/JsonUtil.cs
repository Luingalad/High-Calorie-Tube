using System;
using System.Collections;
using Boomlagoon.JSON;
public class JsonUtil
{
    public static string CollectionToJsonString<T>(T arr, string jsonKeyArr, string jsonKeyDate, long Date) where T : IList
    {
        JSONObject jObject = new JSONObject();
        JSONArray jArray = new JSONArray();
        JSONArray jArrayDate = new JSONArray();       
        
        for(int i = 0; i < arr.Count; i++)
        {
            jArray.Add(new JSONValue(arr[i].ToString()));
        }
        jArrayDate.Add(new JSONValue(Date.ToString()));

        jObject.Add(jsonKeyArr, jArray);
        jObject.Add(jsonKeyDate, jArrayDate);
        return jObject.ToString();
    }
    public static T[] JsonStringToArray<T> (string jsonString, string jsonKey, string jsonKeyDate, 
        Func<string, T> parser1, Func<string, long> parser2, out long date)
    {
        JSONObject jObject = JSONObject.Parse(jsonString);
        JSONArray jArray = jObject.GetArray(jsonKey);
        JSONArray jArrayDate = jObject.GetArray(jsonKeyDate);

        T[] convertedArray = new T[jArray.Length];        

        for (int i = 0; i < jArray.Length; i++)
        {
            convertedArray[i] = parser1(jArray[i].Str.ToString());
        }
        date = parser2(jArrayDate[0].Str.ToString());
        return convertedArray;
    }
}
