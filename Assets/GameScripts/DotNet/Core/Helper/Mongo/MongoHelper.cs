#if TENGINE_NET
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using TrueSync;
using Unity.Mathematics;
using MongoHelper = TEngine.Core.MongoHelper;

namespace TEngine.Core;

public sealed class MongoHelper : Singleton<MongoHelper>
{
    private readonly HashSet<int> _registerCount = new HashSet<int>(3);

    static MongoHelper()
    {
        // 自动注册IgnoreExtraElements
        var conventionPack = new ConventionPack {new IgnoreExtraElementsConvention(true)};
        ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);
        RegisterStruct<float2>();
        RegisterStruct<float3>();
        RegisterStruct<float4>();
        RegisterStruct<quaternion>();
            
        RegisterStruct<FP>();
        RegisterStruct<TSVector>();
        RegisterStruct<TSVector2>();
        RegisterStruct<TSVector4>();
        RegisterStruct<TSQuaternion>();
    }
    
    public static void RegisterStruct<T>() where T : struct
    {
        BsonSerializer.RegisterSerializer(typeof (T), new StructBsonSerialize<T>());
    }

    protected override void OnLoad(int assemblyName)
    {
        if (_registerCount.Count == 3)
        {
            return;
        }

        _registerCount.Add(assemblyName);

        Task.Run(() =>
        {
            foreach (var type in AssemblyManager.ForEach(assemblyName))
            {
                if (type.IsInterface || type.IsAbstract || !typeof(Entity).IsAssignableFrom(type))
                {
                    continue;
                }
                
                BsonClassMap.LookupClassMap(type);
            }
        });
    }
    
    private static readonly JsonWriterSettings defaultSettings = new() { OutputMode = JsonOutputMode.RelaxedExtendedJson };
    
    public static string ToJson(object obj)
    {
        if (obj is ISupportInitialize supportInitialize)
        {
            supportInitialize.BeginInit();
        }
        return obj.ToJson(defaultSettings);
    }

    public static string ToJson(object obj, JsonWriterSettings settings)
    {
        if (obj is ISupportInitialize supportInitialize)
        {
            supportInitialize.BeginInit();
        }
        return obj.ToJson(settings);
    }

    public static T FromJson<T>(string str)
    {
        try
        {
            return BsonSerializer.Deserialize<T>(str);
        }
        catch (Exception e)
        {
            throw new Exception($"{str}\n{e}");
        }
    }

    public static object FromJson(Type type, string str)
    {
        return BsonSerializer.Deserialize(str, type);
    }

    public T Deserialize<T>(byte[] bytes)
    {
        return BsonSerializer.Deserialize<T>(bytes);
    }

    public object Deserialize(byte[] bytes, Type type)
    {
        return BsonSerializer.Deserialize(bytes, type);
    }
    
    public object Deserialize(byte[] bytes, string type)
    {
        return BsonSerializer.Deserialize(bytes, Type.GetType(type));
    }
    
    public T Deserialize<T>(Stream stream)
    {
        return BsonSerializer.Deserialize<T>(stream);
    }
    
    public object Deserialize(Stream stream, Type type)
    {
        return BsonSerializer.Deserialize(stream, type);
    }

    public object DeserializeFrom(Type type, MemoryStream stream)
    {
        return Deserialize(stream, type);
    }

    public T DeserializeFrom<T>(MemoryStream stream)
    {
        return Deserialize<T>(stream);
    }

    public T DeserializeFrom<T>(byte[] bytes, int index, int count)
    {
        return BsonSerializer.Deserialize<T>(bytes.AsMemory(index, count).ToArray());
    }
    
    public byte[] SerializeTo<T>(T t)
    {
        return t.ToBson();
    }

    public void SerializeTo<T>(T t, MemoryStream stream)
    {
        var bytes = t.ToBson();

        stream.Write(bytes, 0, bytes.Length);
    }

    public T Clone<T>(T t)
    {
        return Deserialize<T>(t.ToBson());
    }
}
#endif
#if TENGINE_UNITY
using System;
using System.IO;
namespace TEngine.Core
{
    public sealed class MongoHelper : Singleton<MongoHelper>
    {
        public object DeserializeFrom(Type type, MemoryStream stream)
        {
            return null;
        }
    }
}
#endif