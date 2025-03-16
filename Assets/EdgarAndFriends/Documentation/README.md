# Edgar and Friends Documentation

## Table of Contents
1. [Introduction](#introduction)
2. [Installation](#installation)
3. [Getting Started](#getting-started)
4. [Core Components](#core-components)
5. [Advanced Usage](#advanced-usage)
6. [API Reference](#api-reference)
7. [Troubleshooting](#troubleshooting)

---

## Introduction

Welcome to the **Edgar and Friends** documentation! This Unity asset provides a robust framework for creating, managing, and integrating room templates with advanced features like Large Language Model (LLM) integration and Edgar compatibility. Whether you're building procedural levels or designing custom environments, this asset simplifies the process with modular and extensible components.

---

## Installation

To install the asset:

1. Import the package into your Unity project.
2. Ensure the following files are included in your project:
   - `Assets/EdgarAndFriends/Scripts/RoomTemplateManager.cs`
   - `Assets/EdgarAndFriends/Scripts/RoomTemplateData.cs`
   - `Assets/EdgarAndFriends/Scripts/RoomTemplateParser.cs`
   - `Assets/EdgarAndFriends/Scripts/LLMIntegration.cs`
   - `Assets/EdgarAndFriends/Scripts/LLMResponseValidator.cs`
   - `Assets/EdgarAndFriends/Scripts/EdgarIntegration.cs`
   - `Assets/EdgarAndFriends/Scripts/AssetPlacementManager.cs`
   - `Assets/EdgarAndFriends/Scripts/CustomAssetDatabase.cs`
3. Add the `RoomTemplateManager` component to a GameObject in your scene.
4. Configure the `EdgarIntegration` component to synchronize room templates with Edgar.
5. Use the `LLMIntegration` and `LLMResponseValidator` components for handling LLM responses.

---

## Getting Started

### Basic Setup

1. Add the `RoomTemplateManager` to your scene.
2. Create room templates using the `RoomTemplateCreator` editor window (`Tools > Room Template Creator`).
3. Use the `RoomTemplateGenerator` or `RoomTemplateGeneratorOptimized` editor windows to generate room templates.
4. Integrate with Edgar using the `EdgarIntegration` component.

### Example Code

```csharp
using YourNamespace;

public class ExampleUsage : MonoBehaviour
{
    [SerializeField] private RoomTemplateManager roomTemplateManager;

    private void Start()
    {
        // Add a new room template
        var newTemplate = new RoomTemplate("ExampleRoom", new Vector2(10, 10), somePrefab);
        roomTemplateManager.AddRoomTemplate(newTemplate);

        // Retrieve all room templates
        var templates = roomTemplateManager.GetAllRoomTemplates();
        Debug.Log($"Total templates: {templates.Count}");
    }
}
```

---

## Core Components

### RoomTemplateManager
Manages the creation, retrieval, and deletion of room templates.

- **Methods**:
  - `AddRoomTemplate(RoomTemplate template)`
  - `RemoveRoomTemplate(RoomTemplate template)`
  - `GetAllRoomTemplates()`
  - `FindRoomTemplateByName(string name)`

### RoomTemplateData
Represents the data structure for a room template.

- **Properties**:
  - `TemplateName`: Name of the template.
  - `TemplateSize`: Size of the template.
  - `TemplatePrefab`: Prefab associated with the template.

### EdgarIntegration
Synchronizes room templates with Edgar for advanced procedural generation.

- **Methods**:
  - `SendTemplatesToEdgar()`
  - `ReceiveTemplatesFromEdgar(List<RoomTemplateData> processedTemplates)`

### LLMIntegration
Handles communication with a Large Language Model (LLM).

- **Methods**:
  - `SendRequestToLLM()`
  - `HandleLLMResponse(string response)`

### LLMResponseValidator
Validates and sanitizes responses from the LLM.

- **Methods**:
  - `ValidateResponse(string response)`
  - `SanitizeResponse(string response)`

---

## Advanced Usage

### Batch Processing with RoomTemplateGeneratorOptimized
Use the `RoomTemplateGeneratorOptimized` editor window for batch operations on room templates. This is useful for large-scale procedural generation.

### Custom Asset Database
Organize and categorize assets using the `CustomAssetDatabase` component.

```csharp
var database = FindObjectOfType<CustomAssetDatabase>();
database.AddAsset("Environment", somePrefab);
var assets = database.GetAssetsByCategory("Environment");
```

---

## API Reference

### RoomTemplateManager
```csharp
public void AddRoomTemplate(RoomTemplate template);
public void RemoveRoomTemplate(RoomTemplate template);
public List<RoomTemplate> GetAllRoomTemplates();
public RoomTemplate FindRoomTemplateByName(string name);
```

### RoomTemplateData
```csharp
public string TemplateName { get; set; }
public Vector2 TemplateSize { get; set; }
public GameObject TemplatePrefab { get; set; }
public void Validate();
```

### EdgarIntegration
```csharp
public void SendTemplatesToEdgar();
public void ReceiveTemplatesFromEdgar(List<RoomTemplateData> processedTemplates);
```

---

## Troubleshooting

### Common Issues

- **RoomTemplateManager not found**:
  Ensure the `RoomTemplateManager` component is added to a GameObject in your scene.

- **LLM response is empty**:
  Check the `PromptConfig` settings and ensure the LLM API is reachable.

- **Edgar synchronization fails**:
  Verify that room templates are correctly configured and the `EdgarIntegration` component is active.

### Getting Support

If you encounter issues not covered in this documentation, please [open an issue](https://github.com/teretzdev/edgar-extension/issues) on GitHub.

---
