using System;
using System.Collections;
using System.Collections.Generic;
using Projects.Tile_Game.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    
    //Internal
    private int _row, _col;
    private TileState _state;

    public TileState State => _state;
    
    public void Init(int row, int col)
    {
        _state = TileState.OFF;
        _row = row;
        _col = col;
        UpdateVisuals();
        TileManager.Instance.OnTileClicked += OnBoardChanged;
    }

    public void SetState(TileState state)
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
            case TileState.ON:
                _state = TileState.OFF;
                break;
            case TileState.OFF:
                _state = TileState.ON;
                break;
            case TileState.INVISIBLE or _:
                break;
        }
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        switch (_state)
        {
            case TileState.ON:
                _image.color = Color.red;
                break;
            case TileState.OFF:
                _image.color = Color.black;
                break;
            case TileState.INVISIBLE or _:
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