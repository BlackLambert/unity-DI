# unity-DI
is a lightweight yet mighty [Unity](https://unity.com/de) dependency injection framework having similar functionality as [Zenject](https://github.com/modesttree/Zenject) while providing less overhead.

## Motivation
With [Zenject](https://github.com/modesttree/Zenject) there already is a mighty dependency injection framework for Unity available allowing true dependency inversion in Unity projects. But the framework comes with a lot of overhead and a lot of functionality to dig in. Usually there are multiple ways to tackle one issue which might be confusing. Unity-DI is taking the good sides of Zenject and tries to remove its downsides.

Why using dependency injection anyway? By default Unity is only supporting [dependency inversion](https://en.wikipedia.org/wiki/Dependency_inversion_principle) by the *SerializeField* attribute. This works fine if the dependency is a MonoBehaviour that exists in the same scene or prefab or when refering an asset. But default class instances can't be refered that way. Also cross-prefab dependencies or dependencies to interfaces can not be resolved that way. Dependency injection is a way to achieve dependency inversion.

## Features

### General
- Easy to use syntax inspired by Zenject.
- No use of refelection and therefore less 'black magic'

### Binding
- Use a tailor-made domain specific language to Bind classes to a dependency injection context
- Have the binding logic within custom reusable Installers
- Bind multiple contract classes to one concrete class
- Have a bound class as single instance or create a new instance per resolve
- Have instances created by constructor, method, factory, prefab or simply provide your own instance

### Contexts
- Use AppContext, SceneContext and GameObjectCotext to provide instances only where they are needed
- Resolve Scene dependencies by specifying a SceneContext ID

### Resolving
- Implement the *Injectable* interface for classes that have external dependencies. It is not important whether the implementing class is a MonoBehaviour or a default C# class.
- The *Inject()* method of the *Injectable* interface is called by the DIContext the implementing class is in. No custom call is necessary

## General Scene Hierarchy
<img width="252" alt="Hierarchy within the scene" src="https://user-images.githubusercontent.com/57714553/153228145-bc472e31-e599-4b30-b1c5-f988ade4cf76.png">

1) The *AppContext* initializes the dependency injection. Bindings to this context are valid for the whole application. There should be only one *AppContext* active at the same time.
2) The *SceneContext* initializes the dependeny injection the scene it is active in. Bindings to this context are only valid in this scene. There should be only one *SceneContext* active per scene.
3) The *GameObjectContext* initializes the dependeny injection of its GameObject hierarchy. Bindings to this context are only valid for the scripts of this hierarchy. There can be multiple *GameObjectContext* in a scene. Also *GameObjectContexts* within a *GameObjectContext*-hierarchy are possible.

## Common Use Cases
- A scene depending on another scene
- Creating a prefab instance with injection (table content, enemy spawner)
- Specific data for a prefab instance (e.g. HealthData)
- Loading a scene additionally to the current scenes by also triggering the injection
- Having an interface as dependency within a MonoBehaviour
