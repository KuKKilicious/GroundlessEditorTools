# GroundlessEditorTools
This repository offers Editor Tools used in the development of the project 'Groundless'. Currently it offers item creation and item effect creation tools.

## Table Of Content
1. Installation
2. Usage
3. Known issues:
4. Credits
5. License


### Installation
You can install this package from the Unity Package Manager by following these instructions: [Installing from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
Use this URL: https://github.com/KuKKilicious/GroundlessEditorTools.git

### Usage 
After installing the Package you can open the item creation window, go to **Game->ItemCreation** .

You can add new items with the **Add new Item** button(item name is required).

To add more effects to an item, click on the details button to open the item effect creation window for that particular item.

Sort the item list with the sort button in the top right corner. 

Search for items and details of items with the search button on the top left. 

Path Settings can be edited by going to **Edit->Project Settings->Groundless** in the main Unity window. 


### Known issues 

#### Critical

#### Major

* deleting effect from effects list will not delete linked generated object

#### Minor

* Error: BeginLayoutGroup must be called first(seems to happen inside of OnGui of Odins codebase)

* Error: When EffectCreationWindow was still open when recompiling

* Unity Project Settings can not be opened from within the item creation window

### Credits

Carsten Gedrat https://github.com/KuKKilicious

### License 

MIT License

Copyright (c) [2020] [Carsten Gedrat]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.