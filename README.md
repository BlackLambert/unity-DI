# unity-DI
is a lightweight but mighty Unity dependency injection framework having similar functionality as [Zenject](https://github.com/modesttree/Zenject) while providing less overhead.

## Motivation
With [Zenject](https://github.com/modesttree/Zenject) there already is a mighty dependency injection framework for Unity available allowing true dependency inversion in Unity projects. But the framework comes with a lot of overhead and a lot of functionality to dig in. Usually there are multiple ways to tackle one issue which might be confusing. Unity-DI is taking the good sides of Zenject and tries to remove its downsides.

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
- The *Inject()* method of the *Injectable* interface is called by the DIContext the implementing class is in. No custom call necessary

## Common Use Cases

### General Scene Hierarchy
