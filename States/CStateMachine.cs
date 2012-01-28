// File: CStateMachine
// Purpose: State Machine that handles switching between and stacking game states
// Author: Patrick A. Alvarez
// Original Date: Sat. Jan 28, 2012
// Last Updated: Sat. Jan 28, 2012
// *** Special thanks to White Hat Studios (Full Sail University) 2011 ***
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rain
{
    public class CStateMachine
    {
        // List of State pointers that allows the stacking of states
        List<AGameState> m_vGameStates;

        public CStateMachine()
        {
            m_vGameStates = new List<AGameState>();
        }

        // Updates current state and any other updateable stacked states
        public void UpdateState(float fDTime)
        {
            AGameState _state;
            int nStateCount = this.m_vGameStates.Count;
            for (int nState = 0; nState < nStateCount; nState++)
            {
                _state = this.m_vGameStates[nState];
                if (nState == (nStateCount - 1) || _state.BUpdate == true)
                {
                    _state.Update(fDTime, this);
                }
            }
        }
        // Renders current state and any other renderable stacked states
        public void RenderState()
        {
            AGameState _state;
            int nStateCount = this.m_vGameStates.Count;
            for (int nState = 0; nState < nStateCount; nState++)
            {
                _state = this.m_vGameStates[nState];
                if (nState == (nStateCount - 1) || _state.BRender == true)
                {
                    _state.Render();
                }
            }
        }
        // Changes current states and clears state stack
        public void ChangeState(AGameState pState)
        {
            this.ClearStateStack();
            this.PushState(pState);
        }
        // Adds a state to the State Stack
        public void PushState(AGameState pState)
        {
            if (pState.BInitialized)
            {
                this.m_vGameStates.Add(pState);
                pState.Enter();
            }
        }
        // Pops the top state from the state stack
        public void PopState()
        {
            int nTop = this.m_vGameStates.Count;
            AGameState pState = this.m_vGameStates[nTop];
            if (0 < nTop)
            {
                this.m_vGameStates.Remove(pState);
                pState.Exit();
            }
        }
        // Clears the entire state stack
        public void ClearStateStack()
        {
            int nStateCount = this.m_vGameStates.Count;
            for (int nState = 0; nState < nStateCount; nState++)
            {
                this.PopState();
            }
        }
    }
}
