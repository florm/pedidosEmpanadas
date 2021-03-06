USE [master]
GO
/****** Object:  Database [2018_2C_TP]    Script Date: 9/3/2018 5:01:48 AM ******/
CREATE DATABASE [2018_2C_TP]
GO
ALTER DATABASE [2018_2C_TP] SET COMPATIBILITY_LEVEL = 100
GO
USE [2018_2C_TP]
GO
/****** Object:  Table [dbo].[EstadoPedido]    Script Date: 9/3/2018 5:01:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoPedido](
	[IdEstadoPedido] [int] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_EstadoPedido] PRIMARY KEY CLUSTERED 
(
	[IdEstadoPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GustoEmpanada]    Script Date: 9/3/2018 5:01:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GustoEmpanada](
	[IdGustoEmpanada] [int] NOT NULL,
	[Nombre] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_GustoEmpanada] PRIMARY KEY CLUSTERED 
(
	[IdGustoEmpanada] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GustoEmpanadaDisponiblePedido]    Script Date: 9/3/2018 5:01:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GustoEmpanadaDisponiblePedido](
	[IdPedido] [int] NOT NULL,
	[IdGustoEmpanada] [int] NOT NULL,
 CONSTRAINT [PK_GustoEmpanadaDisponiblePedido] PRIMARY KEY CLUSTERED 
(
	[IdPedido] ASC,
	[IdGustoEmpanada] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvitacionPedido]    Script Date: 9/3/2018 5:01:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvitacionPedido](
	[IdInvitacionPedido] [int] IDENTITY(1,1) NOT NULL,
	[IdPedido] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[Completado] [bit] NOT NULL,
 CONSTRAINT [PK_InvitacionPedido_1] PRIMARY KEY CLUSTERED 
(
	[IdInvitacionPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvitacionPedidoGustoEmpanadaUsuario]    Script Date: 9/3/2018 5:01:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvitacionPedidoGustoEmpanadaUsuario](
	[IdInvitacionPedidoGustoEmpanadaUsuario] [int] IDENTITY(1,1) NOT NULL,
	[IdPedido] [int] NOT NULL,
	[IdGustoEmpanada] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_InvitacionPedidoGustoEmpanadaUsuario] PRIMARY KEY CLUSTERED 
(
	[IdInvitacionPedidoGustoEmpanadaUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 9/3/2018 5:01:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[IdPedido] [int] identity(1,1) NOT NULL,
	[IdUsuarioResponsable] [int] NOT NULL,
	[NombreNegocio] [nvarchar](200) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[IdEstadoPedido] [int] NOT NULL,
	[PrecioUnidad] [int] NOT NULL,
	[PrecioDocena] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[IdPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 9/3/2018 5:01:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](300) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[InvitacionPedido] ADD  CONSTRAINT [DF_InvitacionPedido_Completado]  DEFAULT ((0)) FOR [Completado]
GO
ALTER TABLE [dbo].[Pedido] ADD  CONSTRAINT [DF_Pedido_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[GustoEmpanadaDisponiblePedido]  WITH CHECK ADD  CONSTRAINT [FK_GustoEmpanadaDisponiblePedido_GustoEmpanada] FOREIGN KEY([IdGustoEmpanada])
REFERENCES [dbo].[GustoEmpanada] ([IdGustoEmpanada])
GO
ALTER TABLE [dbo].[GustoEmpanadaDisponiblePedido] CHECK CONSTRAINT [FK_GustoEmpanadaDisponiblePedido_GustoEmpanada]
GO
ALTER TABLE [dbo].[GustoEmpanadaDisponiblePedido]  WITH CHECK ADD  CONSTRAINT [FK_GustoEmpanadaDisponiblePedido_Pedido] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedido] ([IdPedido])
GO
ALTER TABLE [dbo].[GustoEmpanadaDisponiblePedido] CHECK CONSTRAINT [FK_GustoEmpanadaDisponiblePedido_Pedido]
GO
ALTER TABLE [dbo].[InvitacionPedido]  WITH CHECK ADD  CONSTRAINT [FK_InvitacionPedido_Pedido] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedido] ([IdPedido])
GO
ALTER TABLE [dbo].[InvitacionPedido] CHECK CONSTRAINT [FK_InvitacionPedido_Pedido]
GO
ALTER TABLE [dbo].[InvitacionPedido]  WITH CHECK ADD  CONSTRAINT [FK_InvitacionPedido_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[InvitacionPedido] CHECK CONSTRAINT [FK_InvitacionPedido_Usuario]
GO
ALTER TABLE [dbo].[InvitacionPedidoGustoEmpanadaUsuario]  WITH CHECK ADD  CONSTRAINT [FK_InvitacionPedidoGustoEmpanadaUsuario_GustoEmpanada] FOREIGN KEY([IdGustoEmpanada])
REFERENCES [dbo].[GustoEmpanada] ([IdGustoEmpanada])
GO
ALTER TABLE [dbo].[InvitacionPedidoGustoEmpanadaUsuario] CHECK CONSTRAINT [FK_InvitacionPedidoGustoEmpanadaUsuario_GustoEmpanada]
GO
ALTER TABLE [dbo].[InvitacionPedidoGustoEmpanadaUsuario]  WITH CHECK ADD  CONSTRAINT [FK_InvitacionPedidoGustoEmpanadaUsuario_Pedido] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedido] ([IdPedido])
GO
ALTER TABLE [dbo].[InvitacionPedidoGustoEmpanadaUsuario] CHECK CONSTRAINT [FK_InvitacionPedidoGustoEmpanadaUsuario_Pedido]
GO
ALTER TABLE [dbo].[InvitacionPedidoGustoEmpanadaUsuario]  WITH CHECK ADD  CONSTRAINT [FK_InvitacionPedidoGustoEmpanadaUsuario_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[InvitacionPedidoGustoEmpanadaUsuario] CHECK CONSTRAINT [FK_InvitacionPedidoGustoEmpanadaUsuario_Usuario]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_EstadoPedido] FOREIGN KEY([IdEstadoPedido])
REFERENCES [dbo].[EstadoPedido] ([IdEstadoPedido])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_EstadoPedido]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Usuario] FOREIGN KEY([IdUsuarioResponsable])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Usuario]
GO
