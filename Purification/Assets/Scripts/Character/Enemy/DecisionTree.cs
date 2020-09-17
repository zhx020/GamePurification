using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree {

    public delegate bool Decision();
    public delegate void Action();

    DecisionTree l_tree;
    DecisionTree r_tree;
    Decision m_Decision;
    Action m_Action;

    // Constructor
    public DecisionTree(){
        l_tree = null;
        r_tree = null;
        m_Decision = null;
        m_Action = null;
    }

    public void SetDecision(Decision new_dec){
        m_Decision = new_dec;
    }

    public void SetAction(Action new_act){
        m_Action = new_act;
    }

    public void SetLeft(DecisionTree new_left){
        l_tree = new_left;
    }

    public void SetRight(DecisionTree new_right){
        r_tree = new_right;
    }

    /* Make Decision */

    public bool Decide(){
        return m_Decision();
    }

    public void GoLeft(){
        l_tree.Search();
    }

    public void GoRight()
    {
        r_tree.Search();
    }

    public void Search(){
        if (m_Action != null){
            m_Action();
        }
        else if(Decide()){
            GoRight();
        }
        else{
            GoLeft();
        }
    }
}
