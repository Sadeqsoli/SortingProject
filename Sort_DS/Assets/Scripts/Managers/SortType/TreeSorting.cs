using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeSorting : MonoBehaviour, ISort
{
    [SerializeField] SortType sortType;

    Button sendingSortType;

    private void Start()
    {
        sendingSortType = GetComponent<Button>();
        sendingSortType.onClick.AddListener(SendingCurrentSorting);
        Deselect();
    }


    void SendingCurrentSorting()
    {
        EventManager.SortSenderEvent?.Invoke(this);
    }

    public int[] Sort(int[] thaList)
    {
        sendingSortType.image.color = _Color.Y_Olive;
        return new int[5] { 21, 22, 23, 24, 25 };
    }

    public void Deselect()
    {
        sendingSortType.image.color = _Color.Y_LOlive;
    }

    public SortType GetSortType()
    {
        return sortType;
    }


}//EndClassss
public class Tree
{
    public class TreeSort
    {
        private TreeSort left;
        private TreeSort right;
        private int key;
        public TreeSort(int key)
        {
            this.key = key;
        }
        private void invert(TreeSort node)
        {
            if (node.key < key)
            {
                if (left != null)
                    left.invert(node);
                else
                {
                    left = node;
                }
            }
            else
            {
                if (right != null)
                    right.invert(node);
                else
                {
                    right = node;
                }
            }
        }
        private void traverse(TreeVisitor visitor)
        {
            if (left != null)
                left.traverse(visitor);
            visitor.visit(this);
            if (right != null)
                right.traverse(visitor);
        }
        interface TreeVisitor
        {
            void visit(TreeSort node);
        }
        class KeyPrint : TreeVisitor
        {
            public void visit(TreeSort node)
            {
                Debug.Log(" " + node.key);
            }
        }
    }


}