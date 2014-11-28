using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class MoleModel : IScoreObservable
{
    public const string TAG = "MoleModel";

    private List<int> moles;
    public List<int> Moles
    {
        get { return moles; }
        set { moles = value; }
    }

    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    protected static MoleModel instance = new MoleModel();
    public static MoleModel Instance { get { return instance; } }

    private MoleModel()
    {
        this.score = 0;
        this.moles = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            this.moles.Add(i);
        }

        this.scoreObservers = new List<IScoreObserver>();
        //this.renewalObservers = new List<IRenewalObserver>();
    }

    /*
     * followings are member functions
     */

    public void RenewMole()
    {
        ShuffleMoles();
        //NotifyObservers<IRenewalObserver>();
    }

    public void incrementScore()
    {
        score += 10;
        NotifyObservers<IScoreObserver>();
    }

    private void ShuffleMoles()
    {
        int count = 100;
        for (int i = 0; i < count; i++)
        {
            int randIdx = Random.Range(1, 8);

            int tmp = moles[0];
            moles[0] = moles[randIdx];
            moles[randIdx] = tmp;
        }
    }

    void printMoles()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < moles.Count; i++)
            sb.Append(moles[i] + " ");

        Debug.Log(sb.ToString());
    }



    /*
     * followings are member functions
     */
    private List<IScoreObserver> scoreObservers; // 각 옵저버 상태 구분은 state pattern
    //private List<IRenewalObserver> renewalObservers; // 각 옵저버 상태 구분은 state pattern

    public void NotifyObservers<TObserver>()
    {
        //if (typeof(TObserver) is IRenewalObserver)
        //{
        //    //renewalObservers.ForEach((o) => { o.Refresh<TObserver>(); });
        //}
        if (typeof(TObserver) is IScoreObserver)
        {
            scoreObservers.ForEach((o) => { o.Refresh<TObserver>(); });
        }
    }

    public void HasChanged()
    {
        throw new System.NotImplementedException();
    }

    public void SetChanged()
    {
        throw new System.NotImplementedException();
    }

    //public void RegisterObserver(IRenewalObserver o)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void RemoveObserver(IRenewalObserver o)
    //{
    //    throw new System.NotImplementedException();
    //}

    public void RegisterObserver(IScoreObserver o)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveObserver(IScoreObserver o)
    {
        throw new System.NotImplementedException();
    }
}
