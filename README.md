# The Shield.Framework

This is a platform agnositc application framework that combines ideologies from the following:
* The Microsoft MVC framework
* The Prism Library
* The Caliburn.Micro framework
* The CODE.Framework
* The Calcium.Core framework
* The MVVMLight framework

Supported features:
* Application bootstrapping
  * Platform agnostic entry-points  
* Application extensibility (Plugin/Module)
  * Similar to [Prism Modules](http://prismlibrary.readthedocs.io/en/latest/WPF/04-Modules/)
* Inversion of Control (Dependency Injection)
  * Built-in IoC Container (Defualt)
  * Ninject
  * Unity
* Messaging aggrigation
  * Publish–subscribe pattern 
  * Ability to utilize a Message broker service
	* Built-in Message broker service (Default)
	* RabbitMQ	
	* ZeroMQ
* Thread dispatching
  * UI
  * Background
  * Seperate SynchronizationContext
* Supported data serialization formats
	* XML
	* Json
	* Binary
	* ProtoBuf
	* MsgPack
* Static variable/parameter guard system
* Static IoC Container access
* Static platform functionality access
	* Thread dispatching
	* Environement information
		* Operating system
		* Runtime
	* Storage
		* Private/Isolated
		* Local
		* Network
		* Virtual filesystem aggregation (VFS)
			* Ability to mount storage points
			* Access all storage under one filesystem


Supported Platforms:
* Windows*
* Linux*
* MacOS*
* Android*
* iOS*
* Web (application and api)*
* Monogame

*planned to be implemented

The included sandboxes show a default/empty application for each platform

### Prerequisites
* To-do

### Installing
* To-do

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/chaosnhatred/epa-envirofacts-api/tags). 

## Authors

* **Bryan Longacre** - *Initial work* - [Chaosnhatred](https://github.com/chaosnhatred)

See also the list of [contributors](https://github.com/chaosnhatred/epa-envirofacts-api/contributors) who participated in this project.

## License

[![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)