Shared Assemblies

Assembly				Depends On
TwoCents.Azure.Library.Validation		-
EntityModel.Archibus.v10		TwoCents.Azure.Library.Validation
EntityModel.Common.v10			-
TwoCents.Azure.Library.WebAPI		-
TwoCents.Azure.Library.Error		TwoCents.Azure.Edmx.Common.dll
TwoCents.Azure.Library.Process		TwoCents.Azure.Library.Error.dll
					TwoCents.Azure.Edmx.Common.dll

Currently Debug versions in root folder

Gateways and ServiceBus components

VW.Common.ProcessManager		TwoCents.Azure.Edmx.Common.dll
					TwoCents.Azure.Library.Error.dll

VW.Archibus.GatewayService		EntityModel.Archibus
					TwoCents.Azure.Library.Error.dll
					TwoCents.Azure.Library.Process
					TwoCents.Azure.Library.WebAPI
VW.Common.Archiving			TwoCents.Azure.Library.Error.dll
VW.Common.Monitoring			TwoCents.Azure.Library.Error.dll

Dev: Deployed based on Debug versions
Prod: Deployed based on Debug versions

