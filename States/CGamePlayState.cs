using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rain
{
    public class CGamePlayState : AGameState
    {
         override public void Enter()
        {
        }
        override public void Input()
        {
        }
        override public void Update(float fDTime, CStateMachine _SM) 
        {
           /* controller.update();

            handleControls(gameTime);
            player.update(gameTime);
            foreach (KeyValuePair<String, Layer> layer in layers)
            {
                layers[layer.Key].update(gameTime);
            }
            updateCamera(gameTime);*/
        }
        override public void Render() { }
        override public void Exit() { }
    }
    
}
