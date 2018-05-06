

# Embedding Unity WebGL build in a react website w/ Firebase

## Instructions

1. Build Unity Project
    1. Open `UnityProject` in Unity
    2. Build as WebGL to `app/assets/unity` (using any template)
        * go to the `app/assets` folder
        * choose to name it `unity`
1. Build website
    1. Run `npm install`
    1. Run `npm start` + open the resulting website
1. Done!

## Notes + TODOs

* Firebase support has not yet been added
* Code for embedding Unity is currently in the `App` component
    * see [[Unity Manual] WebGL Build](https://docs.unity3d.com/Manual/webgl-building.html)
* The current version displays (almost exclusively) the Unity game
* There is an HTML button beneath it to spawn objects in the game
    * see [[Unity Manual] WebGL build interacting with browser scripting](https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html)



# ES6 React boilerplate using Webpack

This code is based on [Minimalistic ES6 React boilerplate](https://github.com/KleoPetroff/react-webpack-boilerplate)

### Available Commands

- `npm start` - start the dev server
- `npm clean` - delete the dist folder
- `npm run production` - create a production ready build in `dist` folder
- `npm run lint` - execute an eslint check
- `npm test` - run all tests
- `npm run test:watch` - run all tests in watch mode
- `npm run coverage` - generate code coverage report in the `coverage` folder

### Vendor Exporting

You can export specific vendors in separate files and load them. All vendors should be included in `app/vendors` and will be exported in a `vendors` folder under `dist`. The main idea is to serve independent JavaScript and CSS libraries, though currently all file formats are supported.

! Don't forget to add the vendors in `app/index.html` and `build/index.html`.

### Production code

Run `npm run production`. The production-ready code will be located under `dist` folder.


