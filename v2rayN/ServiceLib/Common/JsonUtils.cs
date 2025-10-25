namespace ServiceLib.Common;

public class JsonUtils
{
    private static readonly string _tag = "JsonUtils";

    private static readonly JsonSerializerOptions _defaultDeserializeOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip
    };

    private static readonly JsonSerializerOptions _defaultSerializeOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private static readonly JsonSerializerOptions _nullValueSerializeOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private static readonly JsonDocumentOptions _defaultDocumentOptions = new()
    {
        CommentHandling = JsonCommentHandling.Skip
    };

    // Default AOT JsonSerializerContext for Native AOT support
    private static JsonSerializerContext? _defaultAotContext;

    /// <summary>
    /// Set the default JsonSerializerContext for AOT scenarios
    /// </summary>
    public static void SetDefaultAotContext(JsonSerializerContext context)
    {
        _defaultAotContext = context;
    }

    /// <summary>
    /// Get the default AOT context (for Native AOT scenarios)
    /// </summary>
    public static JsonSerializerContext? GetDefaultAotContext()
    {
        return _defaultAotContext;
    }

    /// <summary>
    /// DeepCopy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepCopy<T>(T obj)
    {
        return Deserialize<T>(Serialize(obj, false))!;
    }

    /// <summary>
    /// Deserialize to object
    /// Automatically uses AOT-friendly context if available
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="strJson"></param>
    /// <returns></returns>
    public static T? Deserialize<T>(string? strJson)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(strJson))
            {
                return default;
            }

            // Try to use AOT context if available
            if (_defaultAotContext != null)
            {
                var typeInfo = (JsonTypeInfo<T>?)_defaultAotContext.GetTypeInfo(typeof(T));
                if (typeInfo != null)
                {
                    return JsonSerializer.Deserialize(strJson, typeInfo);
                }
            }

            // Fallback to reflection-based deserialization
            return JsonSerializer.Deserialize<T>(strJson, _defaultDeserializeOptions);
        }
        catch (Exception ex)
        {
            Logging.SaveLog(_tag, ex);
            return default;
        }
    }

    /// <summary>
    /// Deserialize to object with JsonTypeInfo (AOT-friendly)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="strJson"></param>
    /// <param name="jsonTypeInfo"></param>
    /// <returns></returns>
    public static T? Deserialize<T>(string? strJson, JsonTypeInfo<T> jsonTypeInfo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(strJson))
            {
                return default;
            }
            return JsonSerializer.Deserialize(strJson, jsonTypeInfo);
        }
        catch (Exception ex)
        {
            Logging.SaveLog(_tag, ex);
            return default;
        }
    }

    /// <summary>
    /// parse
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    public static JsonNode? ParseJson(string strJson)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(strJson))
            {
                return null;
            }
            return JsonNode.Parse(strJson, nodeOptions: null, _defaultDocumentOptions);
        }
        catch
        {
            //SaveLog(ex.Message, ex);
            return null;
        }
    }

    /// <summary>
    /// Serialize Object to Json string
    /// Automatically uses AOT-friendly context if available
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="indented"></param>
    /// <param name="nullValue"></param>
    /// <returns></returns>
    public static string Serialize(object? obj, bool indented = true, bool nullValue = false)
    {
        var result = string.Empty;
        try
        {
            if (obj == null)
            {
                return result;
            }

            // Try to use AOT context if available
            if (_defaultAotContext != null && obj != null)
            {
                var typeInfo = _defaultAotContext.GetTypeInfo(obj.GetType());
                if (typeInfo != null)
                {
                    result = JsonSerializer.Serialize(obj, typeInfo);
                    return result;
                }
            }

            // Fallback to reflection-based serialization
            var options = nullValue ? _nullValueSerializeOptions : _defaultSerializeOptions;
            result = JsonSerializer.Serialize(obj, options);
        }
        catch (Exception ex)
        {
            Logging.SaveLog(_tag, ex);
        }
        return result;
    }

    /// <summary>
    /// Serialize Object to Json string with JsonTypeInfo (AOT-friendly)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="jsonTypeInfo"></param>
    /// <returns></returns>
    public static string Serialize<T>(T obj, JsonTypeInfo<T> jsonTypeInfo)
    {
        var result = string.Empty;
        try
        {
            if (obj == null)
            {
                return result;
            }
            result = JsonSerializer.Serialize(obj, jsonTypeInfo);
        }
        catch (Exception ex)
        {
            Logging.SaveLog(_tag, ex);
        }
        return result;
    }

    /// <summary>
    /// Serialize Object to Json string
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string Serialize(object? obj, JsonSerializerOptions options)
    {
        var result = string.Empty;
        try
        {
            if (obj == null)
            {
                return result;
            }
            result = JsonSerializer.Serialize(obj, options);
        }
        catch (Exception ex)
        {
            Logging.SaveLog(_tag, ex);
        }
        return result;
    }

    /// <summary>
    /// SerializeToNode
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static JsonNode? SerializeToNode(object? obj, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.SerializeToNode(obj, options);
    }
}
