﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_Door : MonoBehaviour
{
    public RG_DoorDirection doorDirection;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
