## Overview

NuklearSharp is C# Port of the ANSI C GUI Library: https://github.com/vurtun/nuklear

It is important to note, that this project is **port**, not **wrapper**. Original C code had been ported to C#. So NuklearSharp doesnt require any native binaries.

## Quick Start for MonoGame
1. Create a new MonoGame project.
2. Add following code to the constructor:
```c#
	IsMouseVisible = true;
	Window.AllowUserResizing = true;
	graphics.PreferredBackBufferWidth = 1024;
	graphics.PreferredBackBufferHeight = 768;
```	
3. [Add reference to the NuklearSharp and NuklearSharp.MonoGame](https://github.com/rds1983/NuklearSharp/wiki/Adding-Reference-to-NuklearSharp).
4. Add following fields to the Game class:
```c#
	private NuklearContext _nuklearContext;
	private Color _background = Color.Black;
	private bool _isTea = false;
```
5. Add following code the LoadContent method:
```c#
	// Create NuklearContext
	_nuklearContext = new NuklearContext(GraphicsDevice);

	// Set default font
	var fontAtlas = new FontAtlasWrapper(_nuklearContext);
	var font = fontAtlas.AddDefaultFont(22);
	fontAtlas.Bake();

	_nuklearContext.SetFont(font);
```
6. Finally replace code of Draw method with following:
```c#			
	GraphicsDevice.Clear(_background);

	// TODO: Add your drawing code here
	if (_nuklearContext.BeginTitled("demo2", "demo2", new Rectangle(50, 50, 600, 400),
		Nuklear.NK_WINDOW_BORDER | Nuklear.NK_WINDOW_MOVABLE | Nuklear.NK_WINDOW_SCALABLE |
		Nuklear.NK_WINDOW_MINIMIZABLE | Nuklear.NK_WINDOW_TITLE))
	{
		_nuklearContext.LayoutRowStatic(30, 80, 1);
		_nuklearContext.LayoutRowDynamic(30, 1);
		_nuklearContext.ButtonText("Button");
		_nuklearContext.LayoutRowDynamic(30, 2);
		if (_nuklearContext.OptionLabel("Tea", _isTea))
			_isTea = true;

		if (_nuklearContext.OptionLabel("Coffee", !_isTea))
			_isTea = false;

		_nuklearContext.ButtonColor(Color.Red);
		_nuklearContext.LayoutRowDynamic(30, 1);
		_nuklearContext.LayoutRowDynamic(30, 2);
		_nuklearContext.LabelColored("background", Nuklear.NK_TEXT_LEFT, _background);

		if (_nuklearContext.ComboBeginColor(_background, new Vector2(_nuklearContext.WidgetWidth(), 400)))
		{
			_nuklearContext.LayoutRowDynamic(120, 1);
			_background = _nuklearContext.ColorPicker(_background, 0);
			_nuklearContext.LayoutRowDynamic(25, 1);
			_background.R = (byte) _nuklearContext.Propertyi("#R", 0, _background.R, 255, 1, 1);
			_background.G = (byte) _nuklearContext.Propertyi("#G", 0, _background.G, 255, 1, 1);
			_background.B = (byte) _nuklearContext.Propertyi("#B", 0, _background.B, 255, 1, 1);
			_background.A = (byte) _nuklearContext.Propertyi("#A", 0, _background.A, 255, 1, 1);
			_nuklearContext.ComboEnd();
		}

		_nuklearContext.LayoutRowDynamic(30, 1);
		_nuklearContext.LabelColored("Sichem Allocated: " + Pointer.AllocatedTotal, Nuklear.NK_TEXT_LEFT, _background);

	}
	_nuklearContext.End();

	_nuklearContext.Draw();

	base.Draw(gameTime);
```
7. Run and observe the following result: ![](/Screenshots/sample.gif)
8. Full source code of the above sample is available here: [NuklearTest.zip](https://github.com/rds1983/NuklearSharp/releases/download/0.1.0.7/NuklearTest.zip)

## Credits
* [nuklear](https://github.com/vurtun/nuklear)
* [stb](https://github.com/nothings/stb)
* [ClangSharp](https://github.com/Microsoft/ClangSharp)
* [sealang](https://github.com/pybee/sealang)
* [MonoGame](http://www.monogame.net/)

Also I would like to thank [@raizam](https://github.com/raizam) as the whole project was his idea. Also he donated the code that was used to make NuklearSharp.MonoGame and RaizamTest.

## License
```
------------------------------------------------------------------------------
This software is available under 2 licenses -- choose whichever you prefer.
------------------------------------------------------------------------------
ALTERNATIVE A - MIT License
Copyright (c) 2017 Micha Mettke
Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
------------------------------------------------------------------------------
ALTERNATIVE B - Public Domain (www.unlicense.org)
This is free and unencumbered software released into the public domain.
Anyone is free to copy, modify, publish, use, compile, sell, or distribute this
software, either in source code form or as a compiled binary, for any purpose,
commercial or non-commercial, and by any means.
In jurisdictions that recognize copyright laws, the author or authors of this
software dedicate any and all copyright interest in the software to the public
domain. We make this dedication for the benefit of the public at large and to
the detriment of our heirs and successors. We intend this dedication to be an
overt act of relinquishment in perpetuity of all present and future rights to
this software under copyright law.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
-----------------------------------------------------------------------------
```

