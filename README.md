# Multiple_File_CUI
Console application used to run an executable file on multiple input files and outputting them where specified, keeping folder structure. Optionally filter input images based on resolution.

## Requirements
* .NET Core 3.1
* [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common)
## Example of Usage
```shell
.\Multiple_File_CUI.exe Example.exe .\input\ .\output\ Y -i [i] -o [o] min[256x256]
```
This will run the following commands (Assming the images meet the resolution requirements):
```shell
.\Example.exe -i .\input\1.png -o .\output\1.png
.\Example.exe -i .\input\2.png -o .\output\2.png
.\Example.exe -i .\input\3.png -o .\output\3.png
```
## Help Explanation
```
Runs multiple files in a given input folder (optionally including subfolders) including arguments through a executable file. Places output in given output path, keeping folder structure. Optionally allows you to set a specific/minimum/maximum image resolution/dimension to only run through the given executable.
REQUIRES System.Drawing.Common(System.Drawing.Common.dll) for image resolution options mentioned below.
Arguments:
1       -       Path to Executable file
2       -       Path to INPUT FILES
3       -       Path to OUTPUT FOLDER
4       -       Y/N run on subfolders
5..n    -       Arguments
                To include INPUT FILES use [i] where they will be subsituted into
                To include OUTPUT FILES use [o] where they will be subsituted into
                To set a specific/minimum/maximum image resolution, use set[128x128]/min[128x128]/max[128x128] respectively (in this case the reoslution given is 128 width and 128 height)
                (include - or / in front of argument if needed)

                example: .\Multiple_File_CUI.exe .\example.exe .\input .\output N -i [i] -o [o]
```
## Misc
I mostly made this program for my own personal usage. It was used along with [waifu2x-caffe's](https://github.com/lltcggie/waifu2x-caffe) CUI to only upscale certain images I wanted to based on their resolution. Since waifu2x-caffe only supports bulk image upscaling indiscriminately in a single folder, I had to create this program to suit my needs. I had a bunch of game file .pngs I wanted to upscale and then reimport back into the game to increase the resolution of the textures the game used. I needed a way to upscale only certain images based on their resolution because I did not want to upscale already high-resolution images, thus bloating the textures of the game. I also needed the program to keep the folder structure of the input files so I can overwrite the old files in their original folder structure with a simple copy and paste.
