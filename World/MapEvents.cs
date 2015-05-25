using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelotV2.World
{
    public class MapEvents
    {
        public MapEvents(Map parentMap)
        {
            this.parentMap = parentMap;
        }
        Map parentMap;

        internal delegate void OnEnterMap(Object.Object playr);
        internal event OnEnterMap _FenterMap;
        internal delegate void OnLeaveMap(Object.Object playr);
        internal event OnLeaveMap _FleaveMap;
        internal delegate void OnTakeDamge(Object.Living obj);
        internal event OnTakeDamge _takeDamage;
        internal delegate void OnFac(Object.Mobile obj);
        internal event OnFac _face;
        internal delegate void OnWlk(Object.Mobile obj);
        internal event OnWlk _walk;
        internal delegate void OnTelee(Object.Mobile obj);
        internal event OnTelee _tele;
        internal delegate void OnSwng(Object.Mobile obj, int face);
        internal event OnSwng _swing;
        internal delegate void OnChgObjSprite(Object.Mobile obj);
        internal event OnChgObjSprite _chgObjSprite;
        internal delegate void OnCurveMagic(Object.Mobile obj, int x, int y, Object.SpellSequence seq);
        internal event OnCurveMagic _CurveMagic;
        internal delegate void OnMagicEffect(Object.Mobile obj, int effect);
        internal event OnMagicEffect _MagicEffect;
        internal delegate void OnTimeCycle(int time);
        internal event OnTimeCycle _TimeCycle;

        public void OnFace(Object.Mobile obj)
        {
            if (_face != null)
                _face(obj);
        }

        public void OnWalk(Object.Mobile obj)
        {
          //  if (parentMap.sizeX < obj.Position.X || parentMap.sizeY < obj.Position.Y)
          //      return;
            if (_walk != null)
                _walk(obj);
        }

        public void OnSwing(Object.Mobile obj, int face)
        {
            if (_swing != null)
                _swing(obj, face);
        }

        public void OnTele(Object.Mobile obj)
        {
            if (_tele != null)
                _tele(obj);
        }

        public void OnChgObjSprit(Object.Mobile obj)
        {
            if (_chgObjSprite != null)
                _chgObjSprite(obj);
        }

        public void OnTimeCyclee(int amt)
        {
            if (_TimeCycle != null)
                _TimeCycle(amt);
        }
        
        public void OnMagcEffect(Object.Mobile obj, int effect)
        {
            if (_MagicEffect != null)
                _MagicEffect(obj, effect);
        }

        public void OnCurvMagic(Object.Mobile obj, int x, int y, Object.SpellSequence seq)
        {
            if (_CurveMagic != null)
                _CurveMagic(obj,x,y,seq);
        }

        public void OnTakeDamage(Object.Living obj)
        {
            if (_takeDamage != null)
                _takeDamage(obj);
            if (obj is Player.Player)
            {
                (obj as Player.Player).gameLink.Send(new Network.GameOutMessage.SetHP(obj as Player.Player).Compile());
            }
        }

        public void OnEnter(Object.Mobile obj)
        {
            if (_FenterMap != null)
                _FenterMap(obj);
        }

        public void OnLeave(Object.Object obj)
        {
            if (_FleaveMap != null)
                _FleaveMap(obj);
        }

        public void RegisterListeners(Player.Player obj)
        {
            var playr = obj as Player.Player;

            _FenterMap += new OnEnterMap(playr.OnEnterMap);
            _FleaveMap += new OnLeaveMap(playr.OnLeaveMap);
            _takeDamage += new OnTakeDamge(playr.OnTakeDamage);
            _face += new OnFac(playr.OnFace);
            _walk += new OnWlk(playr.OnWalk);
            _tele += new OnTelee(playr.OnTele);
            _swing += new OnSwng(playr.OnSwing);
            _chgObjSprite += new OnChgObjSprite(playr.OnChangeObjSprite);
            _CurveMagic += new OnCurveMagic(playr.OnCurveMagic);
            _MagicEffect += new OnMagicEffect(playr.OnMagicEffect);
            _TimeCycle += new OnTimeCycle(playr.OnTimeCycle);

        }

        public void RemoveListeners(Player.Player obj)
        {
            var playr = obj as Player.Player;

            _FenterMap -= new OnEnterMap(playr.OnEnterMap);
            _FleaveMap -= new OnLeaveMap(playr.OnLeaveMap);
            _takeDamage -= new OnTakeDamge(playr.OnTakeDamage);
            _face -= new OnFac(playr.OnFace);
            _walk -= new OnWlk(playr.OnWalk);
            _tele -= new OnTelee(playr.OnTele);
            _swing -= new OnSwng(playr.OnSwing);
            _chgObjSprite -= new OnChgObjSprite(playr.OnChangeObjSprite);
            _CurveMagic -= new OnCurveMagic(playr.OnCurveMagic);
            _MagicEffect -= new OnMagicEffect(playr.OnMagicEffect);
            _TimeCycle -= new OnTimeCycle(playr.OnTimeCycle);
        }
    }
}
