import React, { Component } from 'react';
import img from '../assets/images/react_logo_512x512.png';

import s from '../assets/scss/main.scss';


//import _nothing from '../assets/unity/Build/UnityLoader';
//import unityBuild from '../assets/';

export default class App extends Component {
  componentDidMount() {
    this.initUnity();
  }

  initUnity() {
    this.gameInstance = window.UnityLoader.instantiate('unityContainer', 'unity/Build/unity.json');
  }

  spawnOne = () => {
    this.gameInstance.SendMessage('Spawner', 'SpawnOne');
  }

  render() {
    return (
      <div>
        <h2 className="heading">Hello Unity Game! <img
          className="image"
          style={{ margin: '0.5em' }}
          height="40"
          width="40"
          src={img}
          alt="React Logo"
        />
        </h2>
        {/* <pre>
          {JSON.stringify(unityBuild, null, 2)}
        </pre> */}
        <div>
          <button type="button" onClick={this.spawnOne}>Spawn!</button>
        </div>
        <div id="unityContainer" className="unity-container" />
      </div>
    );
  }
}
