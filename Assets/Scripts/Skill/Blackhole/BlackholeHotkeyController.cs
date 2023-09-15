using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Skill.Blackhole
{
    public class BlackholeHotkeyController : MonoBehaviour
    {
        private SpriteRenderer sr;
        private KeyCode myHotkey;
        private TextMeshPro myText;
        private Enemy.Enemy enemy;
        private Blackhole blackholeSkill;
        public void SetupHotkey(KeyCode keyCode, Enemy.Enemy enemy, Blackhole blackholeSkill)
        {
            sr = GetComponent<SpriteRenderer>();
            myText = GetComponentInChildren<TextMeshPro>();
            myText.text = keyCode.ToString();
            myHotkey = keyCode;
            this.enemy = enemy;
            this.blackholeSkill = blackholeSkill;
        }

        private void Update()
        {
            if (Input.GetKeyDown(myHotkey))
            {
                blackholeSkill.AddEnemyToList(enemy.transform);
                myText.color = Color.clear;
                sr.color = Color.clear;
            }
        }
    }
}