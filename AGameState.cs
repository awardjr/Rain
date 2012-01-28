using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rain
{
    // Abstract version of a game state
    abstract public class AGameState
    {
        AGameState m_PrevState;
        internal AGameState PrevState
        {
            get { return m_PrevState; }
            set { m_PrevState = value; }
        }
        
        Boolean m_bUpdate;
        public Boolean BUpdate
        {
            get { return m_bUpdate; }
            set { m_bUpdate = value; }
        }
        
        Boolean m_bRender;
        public Boolean BRender
        {
            get { return m_bRender; }
            set { m_bRender = value; }
        }

        Boolean m_bInitialized;
        public Boolean BInitialized
        {
            get { return m_bInitialized; }
        }

        public AGameState()
        {
            m_PrevState = null;
            m_bUpdate = false;
            m_bRender = true;
            m_bInitialized = true;
        }

        abstract public void Enter();
        abstract public void Input();
        abstract public void Update(float fDTime, CStateMachine _SM);
        abstract public void Render();
        abstract public void Exit();
    }
}
