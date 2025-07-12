using System;
using System.Collections;
using System.Collections.Generic;
using Projects.Tile_Game.Scripts;
using Tools.Scripts.Inspector.Multidimensional_Arrays;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    
    //Internal
    private int _row, _col;
    private Active _state;

    public Active State => _state;
    
    public void Init(int row, int col)
    {
        _state = Active.Disabled;
        _row = row;
        _col = col;
        UpdateVisuals();
        TileManager.Instance.OnTileClicked += OnBoardChanged;
    }

    public void SetState(Active state)
    {
        _state = state;
        UpdateVisuals();
    }

    private void OnBoardChanged(int tileRow, int tileCol)
    {
        if (IsAffectedByToggle(tileRow, tileCol))
        {
            UpdateState();
        }
    }

    private bool IsAffectedByToggle(int tileRow, int tileCol)
    {
        bool sameRow = tileRow == _row;
        bool sameCol = tileCol == _col;
        
        bool isSelf = sameRow && sameCol;
        bool toRight = sameRow && tileCol == _col+1;
        bool toLeft = sameRow && tileCol == _col-1;
        bool isAbove = sameCol && tileRow == _row-1;
        bool isBelow = sameCol && tileRow == _row+1;
        return isSelf || toRight || toLeft || isAbove || isBelow;
    }

    private void UpdateState()
    {
        switch (_state)
        {
            case Active.Enabled:
                _state = Active.Disabled;
                break;
            case Active.Disabled:
                _state = Active.Enabled;
                break;
            case Active.Unavailable or _:
                break;
        }
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        switch (_state)
        {
            case Active.Enabled:
                _image.color = Color.red;
                break;
            case Active.Disabled:
                _image.color = Color.black;
                break;
            case Active.Unavailable or _:
                _image.color = Color.clear;
                return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TileManager.Instance.InvokeOnTileClicked(_row,_col);
    }
}

[System.Serializable]
public enum TileState
{
    OFF,
    ON,
    INVISIBLE,
}