using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager {
    public const int NodeSize = 40;
    public static PathNode[,] mNodeArr;
    public static int Width;
    public static int Height;
    public static void InitData(int width, int height) {
        PathManager.Width = width;
        PathManager.Height = height;
        mNodeArr = new PathNode[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                mNodeArr[x, y] = new PathNode(x, y);
            }
        }

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (y + 1 < height) {
                    mNodeArr[x, y].Up = mNodeArr[x, y + 1];
                }
                if (y > 0) {
                    mNodeArr[x, y].Down = mNodeArr[x, y - 1];
                }
                if (x > 0) {
                    mNodeArr[x, y].Left = mNodeArr[x - 1, y];
                }
                if (x + 1 < width) {
                    mNodeArr[x, y].Right = mNodeArr[x + 1, y];
                }
            }
        }
    }

    private static List<PathNode> __list = new List<PathNode>();
    public static PathNode GetInitNode(bool is_hero) {
        int x;
        if (is_hero) {
            x = 0;
        } else {
            x = PathManager.Width - 1;
        }
        __list.Clear();
        for (int i = 0; i < PathManager.Height; i++) {
            if (mNodeArr[x, i].IsLock == false) {
                __list.Add(mNodeArr[x, i]);
            }
        }
        if (__list.Count > 0) {
            return __list[Random.Range(0, __list.Count)];
        } else {
            return mNodeArr[x, Random.Range(0, PathManager.Height)];
        }
    }
}

public class PathNode {
    public int x;
    public int y;

    public List<GameObject> mLockList = new List<GameObject>();

    public PathNode Up;
    public PathNode Down;
    public PathNode Left;
    public PathNode Right;

    public bool IsLock{
        get{
            return this.mLockList.Count > 0;
        }
    }


    public PathNode(int x, int y) {
        this.x = x;
        this.y = y;
    }

    private static List<PathNode> __list = new List<PathNode>();
    public PathNode GetNextPath(PathNode target,int range) {
        __list.Clear();
        if (this.y == target.y) {
            if (this.x < target.x) {
                __list.Add(this.Right);
            } else {
                __list.Add(this.Left);
            }
        } else {
            if (Mathf.Abs(this.y - target.y) < Mathf.Abs(this.x - target.x)) {
                if (this.x < target.x) {
                    __list.Add(this.Right);
                } else {
                    __list.Add(this.Left);
                }
                if (this.y < target.y) {
                    __list.Add(this.Up);
                } else {
                    __list.Add(this.Down);
                }
            } else {
                if (this.y < target.y) {
                    __list.Add(this.Up);
                } else {
                    __list.Add(this.Down);
                }
                if (this.x < target.x) {
                    __list.Add(this.Right);
                } else {
                    __list.Add(this.Left);
                }
            }
        }
        foreach (var item in __list) {
            if (item.IsLock == false) {
                return item;
            }
        }
        return null;
    }

    public Vector2 ToTruePos() {
        return new Vector2(this.x * PathManager.NodeSize, this.y * PathManager.NodeSize);
    }

    public void Lock(GameObject obj){
        this.mLockList.Add(obj);
    }
    public void Unlock(GameObject obj){
        this.mLockList.Remove(obj);
    }

    public static int GetWeightLen(PathNode node1, PathNode node2) {//获取权值距离  
        return Mathf.Abs(node1.x - node2.x) + Mathf.Abs(node1.y - node2.y) * 2;
    }
}