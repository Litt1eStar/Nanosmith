﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tween the object's local scale.
/// </summary>

public class TweenScaleTba : TbaTweener {

    public Vector3 from = Vector3.one;
    public Vector3 to = Vector3.one;
    public bool updateTable = false;

    Transform mTrans;

    public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }

    public Vector3 value { get { return cachedTransform.localScale; } set { cachedTransform.localScale = value; } }

    [System.Obsolete("Use 'value' instead")]
    public Vector3 scale { get { return this.value; } set { this.value = value; } }

    /// <summary>
    /// Tween the value.
    /// </summary>

    protected override void OnUpdate(float factor, bool isFinished)
    {
        value = from * (1f - factor) + to * factor;
    }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>

    static public TweenScaleTba Begin(GameObject go, float duration, Vector3 scale)
    {
        TweenScaleTba comp = TbaTweener.Begin<TweenScaleTba>(go, duration);
        comp.from = comp.value;
        comp.to = scale;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    [ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue() { from = value; }

    [ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue() { to = value; }

    [ContextMenu("Assume value of 'From'")]
    void SetCurrentValueToStart() { value = from; }

    [ContextMenu("Assume value of 'To'")]
    void SetCurrentValueToEnd() { value = to; }
}
