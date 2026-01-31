# 🎮 Unity 6 Split-Screen Multiplayer Template

A ready-to-use Unity 6 project for setting up local split-screen multiplayer (2–4 players) using the new Input System and PlayerInputManager. Great for fast prototyping, teaching, or as a base for your own local multiplayer game!

---

## 🧩 Features

- ✅ **Unity 6.0+** compatible  
- 🎮 **Supports 2–4 players** with split-screen layout  
- ⚡ **Modern Input System** with multiple control schemes  
- 👀 **Dynamic camera layout** for 2, 3, or 4 players  
- 🧑‍🤝‍🧑 Supports **keyboard + controller** combinations (2nd keyboard requires custom join code but 3 gamepads and 1 keyboard or 4 gamepads should work)
- 📁 Lightweight and easy to understand

---

## 🚀 Getting Started

1. **Open the Project in Unity 6+**
   - Unity 6.0 or higher is required (new Input System installed by default)

2. **Run the `SampleScene`**
   - Hit Play to test local multiplayer
   - Use connected controllers or keyboard keys to join

3. **Controls**
   - **Player 1**: WASD + Space 
   - **Controllers**: Plug & play (up to 4 players total)

---

## 🧠 How It Works

- `PlayerInputManager` handles player joining
- `PlayerInput` spawns players with the correct control scheme
- `SplitScreenCamera.cs` assigns a unique viewport to each player's camera based on total players and index

---

## 🛠️ Customization Tips

- Add your own player models, abilities, and effects to `PlayerPrefab`
- Modify `PlayerControls.inputactions` to add more actions
- Tweak the `SplitScreenCamera` script to adjust viewport layout

---

## 📂 Project Structure

```

Assets/
├── Scripts/
│   ├── PlayerController.cs
│   ├── SplitScreenCamera.cs
│   └── ...
├── Prefabs/
│   └── Player.prefab
├── Input/
│   └── PlayerControls.inputactions
└── Scenes/
└── SampleScene.unity

```

---

## 📦 Build & Export

To package or distribute:

1. Go to **File > Build Settings**
2. Choose **PC, Mac & Linux Standalone** or your target platform
3. Add `SampleScene` to Scenes in Build
4. Click **Build**

---

## 📧 Support

Questions, feedback, or feature requests?  
Contact: dmak@faktorystudios.com
Or visit: https://www.youtube.com/@FaktoryStudios
```