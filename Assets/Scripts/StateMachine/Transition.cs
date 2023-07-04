using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class Transition
    {
        private State _fromState;
        private State _toState;
        private BoolDeligate _deligate;

        public delegate bool BoolDeligate();

        public State FromState => _fromState;
        public State ToState => _toState;
        public BoolDeligate Deligate => _deligate;

        public Transition(State fromState, State toState, BoolDeligate boolDeligate)
        {
            _fromState = fromState;
            _toState = toState;
            _deligate = boolDeligate;
        }
    }
}

