# Native AOT JSON 序列化支持

## 概述

为了支持 Native AOT 编译，项目中的 JSON 序列化已更新为使用源生成器（Source Generator）。这避免了运行时反射，使应用程序能够在 Native AOT 环境中正常工作。

## 主要改动

### 1. AotJsonContext.cs
创建了一个 `JsonSerializerContext` 类，用于定义所有需要序列化/反序列化的类型。这个文件使用 `[JsonSerializable]` 特性来注册类型。

### 2. JsonUtils.cs 改进
- 添加了 `SetDefaultAotContext()` 和 `GetDefaultAotContext()` 方法
- `Deserialize<T>()` 和 `Serialize()` 方法现在会自动尝试使用 AOT context
- 如果 AOT context 可用，优先使用；否则回退到反射模式
- 新增了接受 `JsonTypeInfo<T>` 参数的重载方法，用于显式指定类型信息

### 3. 应用初始化
在 `Program.cs` (Desktop) 和 `App.xaml.cs` (WPF) 中添加了初始化代码：
```csharp
JsonUtils.SetDefaultAotContext(AotJsonContext.Default);
```

## 如何添加新类型

当你需要序列化/反序列化新的类型时，需要在 `AotJsonContext.cs` 中添加相应的 `[JsonSerializable]` 特性：

```csharp
[JsonSerializable(typeof(YourNewType))]
[JsonSerializable(typeof(List<YourNewType>))] // 如果需要序列化列表
public partial class AotJsonContext : JsonSerializerContext
{
}
```

## 使用方式

### 自动模式（推荐）
无需修改现有代码，`JsonUtils` 会自动使用 AOT context：

```csharp
var obj = JsonUtils.Deserialize<MyType>(jsonString);
var json = JsonUtils.Serialize(myObject);
```

### 显式模式
如果需要显式指定类型信息（性能最优）：

```csharp
var obj = JsonUtils.Deserialize(jsonString, AotJsonContext.Default.MyType);
var json = JsonUtils.Serialize(myObject, AotJsonContext.Default.MyType);
```

## 注意事项

1. **类型注册**：所有需要序列化的类型都必须在 `AotJsonContext.cs` 中注册
2. **嵌套类型**：如果类型包含复杂的嵌套属性，可能需要单独注册这些嵌套类型
3. **泛型集合**：`List<T>`, `Dictionary<K,V>` 等泛型类型需要显式注册具体的泛型参数
4. **向后兼容**：即使在非 AOT 环境中，这些改动也能正常工作（会回退到反射模式）

## 编译要求

- .NET 8.0 或更高版本
- 启用 Native AOT 发布需要在项目文件中添加：
```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
</PropertyGroup>
```

## 错误排查

如果遇到 `System.InvalidOperationException` 错误：
1. 检查相关类型是否已在 `AotJsonContext.cs` 中注册
2. 确认 `JsonUtils.SetDefaultAotContext()` 在应用启动时被调用
3. 查看日志文件中的详细错误信息

## 性能优势

- **启动速度**：避免了运行时类型扫描和元数据生成
- **内存占用**：减少了反射相关的内存开销
- **包大小**：AOT 编译可以进行更激进的裁剪，减小最终包大小
