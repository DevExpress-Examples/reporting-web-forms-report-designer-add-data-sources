﻿Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity
Imports System

'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Namespace WebApplication1

    Partial Public Class NorthwindEntities
        Inherits DbContext

        Public Sub New()
            MyBase.New("name=NorthwindEntities")
        End Sub

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            Throw New UnintentionalCodeFirstException()
        End Sub

        Public Overridable Property Categories() As DbSet(Of Category)
    End Class
End Namespace
