using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AddPhotoPopupViewManager : PopupViewManager
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] GameObject baseImageCell;
    public Action<Sprite> didSelectImage;
    protected override void Awake() {
        base.Awake();
        Sprite[] sprites = SpriteManager.Load();
        MakeImageCell(sprites);
        baseImageCell.SetActive(false);
    }
    private void MakeImageCell(Sprite[] sprites)
    {
        float cellHeight = (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y) * (sprites.Length / gridLayoutGroup.constraintCount) + gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom;
        scrollRect.content.sizeDelta = new Vector2(0, cellHeight);
        foreach(Sprite sprite in sprites)
        {
            ImageCell imageCell = Instantiate(baseImageCell, scrollRect.content).GetComponent<ImageCell>();
            imageCell.SetImageCell(sprite, (selectedSprite) => { didSelectImage?.Invoke(selectedSprite); Close(); });
        }
    }
    public void OnClickClose()
    {
        Close();
    }
}
