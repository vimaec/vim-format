# VIM Object Model

## What is VIM?

VIM is a file format which has been designed to quickly display 3D architectural and engineering designs on modern graphical devices without the need for the original design software. A VIM file contains:
1. The geometric information required to visually render the design in 3D.
2. The BIM information about the objects contained in the design, such as their names, materials, categories, etc.

## What is the VIM Object Model?

The **VIM Object Model** is the data schema which describes the BIM information inside a VIM file. The entities and relations defined in this schema are used to represent the built environment. This schema is based on the concepts and data structures used in Autodesk Revit, an application used by architects and engineers to design buildings.

In this overview, we'll cover the main entities and relationships of the VIM Object Model. For brevity, we'll avoid getting bogged down in the details so you can build an intuition of what's involved.

**Elements and Parameters**

The `Element` entity is at the heart of the VIM Object Model. An `Element` has a name, an id, and a number of relationships to other entities, which we'll get to in a bit. Importantly, an `Element` can have a collection of `Parameters` which flexibly describe the properties of that element.

For example, an element representing a wall could have the name "Exterior - Stucco on Studs" and could have the following `Parameter` entities which define its dimensions (units are in feet by default when they are exported from Revit):
- Length: 20'-3 5/16"
- Area: 485 SF
- Volume: 292.91 CF

Conversely, another element representing a floor has a different set of `Parameter` entities which define the dimensions of that floor object. Note that this floor does not have a "Length", but does have a "Slope" and a "Thickness":
- Slope: 0"/12"
- Thickness: 1'-1 1/2"
- Volume: 352.46 CF
- Area: 313 SF

`Parameter` entities contain a **value**, and have a `ParameterDescriptor` which define the **name** of the parameter, as well as a few other fields including their display units (feet, meters, etc). Using the wall example above, the entity relationships can be visualized as follows:

```
  +------------------------------------+          +---------------------+          +----------------------+
  | Element                            |<---------| Parameter           |--------->| ParameterDescriptor  |
  | Name: "Exterior - Stucco on Studs" |<------.  | Value: 20'-3 5/16"  |          | Name: "Length"       |
  |                                    |<---.  |  +---------------------+          +----------------------+
  +------------------------------------+    |  |
                                            |  |  +---------------------+          +----------------------+
                                            |  .--| Parameter           |--------->| ParameterDescriptor  |
                                            |     | Value: 485 SF       |          | Name: "Area"         |
                                            |     +---------------------+          +----------------------+
                                            |
                                            |     +---------------------+          +----------------------+
                                            .-----| Parameter           |--------->| ParameterDescriptor  |
                                                  | Value: 292.91 CF    |          | Name: "Volume"       |
                                                  +---------------------+          +----------------------+
```

The generic nature of elements and parameters enable the description a wide range of objects and concepts, including:
- Materials, whose parameters define physical properties such as fire resistance rating or thermal insulation.
- Rooms, whose parameters define the area and volume of the room inside the building.
- Levels, whose parameters define the physical elevation values depending on the point of reference.
- Systems, whose parameters may define associations to specific networks such as hot water or cold water plumbing systems, or different airflow directions in the case of ventilation ducts.
- Family Instances, Family Types, and Families, which we'll get to next.

**"Family Instances", "Family Types", and "Families"**

`FamilyInstance`
- Represents a countable object that has a physical presence in the building.
- Contains transformation information about the physical object.
- Has a relationship to an `Element` entity, which defines that family instance's name and parameters.
- Has a relationship to a `FamilyType` entity, which we'll define below.

`FamilyType`
- Represents broad attributes which apply to all of the `FamilyInstance` entities of that family type.
- Has a relationship to an `Element` entity, which defines that family type's name and parameters.
- Has a relationship to a `Family` entity, which we'll define below.

`Family`
- Represents even broader attributes which apply to all of the `FamilyType` entities of that family, and, transitively, to all the `FamilyInstance` entities of that family.
- Has a relationship to an `Element` entity, which defines that family's name and parameters.

The diagram below illustrates the relationships between these entities.
```
   ___________________         +-----------------+             +------------------+             +------------------+          
  |  ______   ______  |        | FamilyInstance  |------------>| FamilyType       |------------>| Family           |          
 .| |      | |      | |        +-----------------+             +------------------+             +------------------+          
 || |      | |      | |          |                               |                                |                           
  | |      | |      | |          |                               |                                |                           
  | |      | |      | |          V                               V                                V                           
  | |______| |______| |        +------------------------+      +------------------------+       +----------------------------+
 .|                   |        | Element                |      | Element                |       | Element                    |
 ||               (O) |        | Id: 459321             |      | Id: 599191             |       | Id: 622173                 |
  |  ______   ______  |        | Name: "36x84-Wood"     |      | Name: "36x84-Wood"     |       | Name: "Residential Single" |
  | |      | |      | |        +------------------------+      +------------------------+       +----------------------------+
 .| |      | |      | |          ^                               ^                                ^                           
 || |      | |      | |          |                               |                                |                           
  | |______| |______| |          |                               |                                |                           
  |___________________|        +------------------------+      +------------------------+       +----------------------------+
                               | Parameter              |      | Parameter              |       | Parameter                  |
                               | Value: Paint-White     |      | Value: 36"             |       | Value: Door                |
                               +------------------------+      +------------------------+       +----------------------------+
                                 |                               |                                |                           
                                 |                               |                                |                           
                                 V                               V                                V                           
                               +--------------------------+    +------------------------+       +----------------------------+
                               | ParameterDescriptor      |    | ParameterDescriptor    |       | ParameterDescriptor        |
                               | Name: Panel Finish       |    | Name: Panel Width      |       | Name: OmniClass Title      |
                               +--------------------------+    +------------------------+       +----------------------------+
```

**BIM Documents**

`BimDocument` entities describe the originating documents used to create a VIM file. In practice, BIM documents commonly refer to Revit files, but may also refer to IFC files. A VIM file may contain more than one BIM document, particularly when a collection of linked Revit files are exported together. BIM documents can represent different parts of the building's design such as the structural components, the mechanical/electrical/plumbing (MEP) components, and the architectural components to name but a few.

## Special Notes

**Entity Index**

Every entity in the VIM Object Model has an "index" field which uniquely identifies the entity within the context of that entity's type and within the scope of that VIM file.

**Element Ids**

The `Element.Id` and `Element.UniqueId` fields are sourced from Revit and a have a few peculiarities:
- `Element.Id`
  - Represents an element's id **within the scope of that element's originating Revit document**. In other words, when a VIM file is created from a collection of linked Revit documents, the same element id values may be repeated.
  - When an element's id is set to -1, this indicates that the element has been created for consistency within the VIM Object Model, but has no related element in the originating BIM document.
- `Element.UniqueId`
  - Represents an element's _slightly more_ unique id (as far as Revit is concerned)
  - It is a field which, according to the Revit API documentation, can be used to correlate the element with persistent storage solutions outside of Revit.
  - In practice, the values of this field have been observed to repeat (and are therefore not so unique), specifically in the case of default or built-in elements in Revit.