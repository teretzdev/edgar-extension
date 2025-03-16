# Edgar and Friends

![Edgar and Friends Banner](Assets/EdgarAndFriends/Documentation/Images/banner.png)

## Overview
Edgar and Friends is a Unity3D asset designed to streamline the creation and management of room templates, integrate with Large Language Models (LLMs), and enhance procedural generation workflows.

## Features
- **Room Template Management**: Easily create, edit, and manage room templates.
- **LLM Integration**: Generate and validate room templates using LLMs.
- **Edgar Integration**: Synchronize room templates with Edgar for advanced procedural generation.
- **Custom Asset Database**: Organize and categorize assets for scalability.
- **Flexible Asset Placement**: Advanced logic for asset positioning within defined areas.
- **Editor Tools**: Includes custom editor windows for room template creation and generation.

## Requirements
- Unity 2020.3 LTS or newer
- No additional dependencies required

## Installation
1. Import the package into your Unity project.
2. Ensure the following files are included in your project:
   - `Assets/EdgarAndFriends/Scripts/RoomTemplateParser.cs`
   - `Assets/EdgarAndFriends/Scripts/LLMResponseValidator.cs`
   - `Assets/EdgarAndFriends/Scripts/EdgarIntegration.cs`
   - `Assets/EdgarAndFriends/Scripts/RoomTemplateManager.cs`
   - `Assets/EdgarAndFriends/Scripts/RoomTemplateData.cs`
   - `Assets/EdgarAndFriends/Scripts/AssetPlacementManager.cs`
   - `Assets/EdgarAndFriends/Scripts/CustomAssetDatabase.cs`
3. Follow these steps to set up the asset:
   - Assign the `RoomTemplateManager` to your scene.
   - Configure the `EdgarIntegration` component to synchronize room templates.
   - Use `LLMResponseValidator` and `RoomTemplateParser` for handling LLM responses.

## Quick Start
```csharp
// Example code showing basic usage
using EdgarAndFriends;

public class ExampleUsage : MonoBehaviour
{
    public RoomTemplateManager roomTemplateManager;
    
    void Start()
    {
        // Initialize the RoomTemplateManager
        roomTemplateManager.AddRoomTemplate(new RoomTemplate("ExampleRoom", new Vector2(10, 10), null));
        
        // Example usage
        var templates = roomTemplateManager.GetAllRoomTemplates();
        Debug.Log($"Number of templates: {templates.Count}");
    }
}
```

## Documentation
For full documentation, please see the [Documentation](Assets/EdgarAndFriends/Documentation/README.md) folder.

## Examples
The asset includes several example scenes demonstrating different features:
- **Basic Example**: Shows core functionality
- **Advanced Example**: Demonstrates more complex usage
- **Integration Example**: Shows how to integrate with Edgar and LLMs

## Support
If you have any questions or issues, please [open an issue](../../issues) on GitHub.

## License
This asset is licensed under the [LICENSE](LICENSE.md) included in this repository.

## Version History
See the [CHANGELOG](CHANGELOG.md) for version history and changes.