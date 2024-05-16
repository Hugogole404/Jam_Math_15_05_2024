using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [Header("--- Setup ---")]
    [SerializeField] private Transform _boosterPack; // Référence au transform du booster pack
    [SerializeField] private Material _boosterMaterial; // Matériau du booster pack
    [SerializeField] private int _touchNeededToOpen;
    [Header("--- Timings Hit ---")]
    [SerializeField] private float _bounceHeight = 0.5f; // Hauteur du rebond
    [SerializeField] private float _bounceDuration = 0.5f; // Durée du rebond
    [SerializeField] private float _rotationAngle = 15f; // Angle de rotation
    [SerializeField] private float _rotationDuration = 0.5f; // Durée de la rotation
    [SerializeField] private float _punchIntensity = 0.5f; // Intensité de l'effet de punch
    [SerializeField] private int _vibrato = 10; // Nombre de vibrations de l'effet de punch
    [SerializeField] private float _scaleDuration = 0.1f; // Durée du changement d'échelle
    [SerializeField] private Color _hitColor; // Couleur de l'effet de "hit"
    [SerializeField] private float _hitDuration = 0.1f; // Durée de l'effet de "hit"

    private Vector3 _startScale; // Taille initiale du booster pack
    private Quaternion _startRotation; // Rotation initiale du booster pack
    private bool _isOpen;

    void Start()
    {
        // Sauvegarder la taille initiale et la rotation initiale du booster pack
        _startScale = _boosterPack.localScale;
        _startRotation = _boosterPack.localRotation;
        OnJumpAnim();
    }

    private void OnJumpAnim()
    {
        var pos = gameObject.transform.position;
        gameObject.transform.DOJump(new Vector3(pos.x, pos.y, pos.z + 5), 2, 1, .5f);
    }

    private void OnMouseDown()
    {
        // Si la touche X est enfoncée et qu'il n'y a pas déjà une animation en cours
        if (Input.GetMouseButton(0))
        {
            _touchNeededToOpen--;

            if (_touchNeededToOpen <= 0)
            {
                OnDeathAnim();
                return;
            }
                
            _boosterPack.DOKill();
            // Lancer l'animation d'ouverture du booster pack
            OpenBoosterPack();
        }
    }

    void OpenBoosterPack()
    {
        if (_isOpen) return;
        // Animation de rebondissement (bounce)
        Sequence bounceSequence = DOTween.Sequence();
        bounceSequence.Append(_boosterPack.DOMoveY(_boosterPack.position.y + _bounceHeight, _bounceDuration / 2f)
                                   .SetEase(Ease.OutQuad)); // Déplacement vers le haut
        bounceSequence.Append(_boosterPack.DOMoveY(_boosterPack.position.y, _bounceDuration / 2f)
                                   .SetEase(Ease.InQuad)); // Retour à la position initiale

        // Animation d'impact (punch scale)
        _boosterPack.localScale = _startScale;
        _boosterPack.DOPunchScale(new Vector3(_punchIntensity, _punchIntensity, _punchIntensity), _scaleDuration, _vibrato)
                   .SetEase(Ease.OutQuad) // Easing pour un effet plus naturel
                   .OnComplete(() =>
                   {
                       // Animation de rotation gauche droite
                       _boosterPack.DOLocalRotateQuaternion(_startRotation * Quaternion.Euler(0, 0, _rotationAngle), _rotationDuration / 2f)
                                  .SetEase(Ease.InOutQuad) // Interpolation quadratique pour un mouvement plus naturel
                                  .SetLoops(2, LoopType.Yoyo) // Boucle une fois pour créer l'effet de rotation gauche droite
                                  .OnComplete(() =>
                                  {
                                      // Réinitialiser l'échelle et la rotation
                                      _boosterPack.DOScale(_startScale, _scaleDuration)
                                                 .SetEase(Ease.InQuad); // Easing pour un effet plus naturel
                                      _boosterPack.DOLocalRotateQuaternion(_startRotation, _rotationDuration / 2f)
                                                  .SetEase(Ease.InQuad); // Easing pour un effet plus naturel
                                  });
                   });

        // Effet de "hit" (changement de couleur)
        if (_boosterMaterial != null)
        {
            Color originalColor = _boosterMaterial.color;
            _boosterMaterial.DOColor(_hitColor, _hitDuration)
                            .SetEase(Ease.OutQuad) // Easing pour un effet plus naturel
                            .OnComplete(() =>
                            {
                                _boosterMaterial.color = originalColor; // Rétablir la couleur d'origine
                            });
        }
    }

    void OnDeathAnim()
    {
        if (_isOpen) return;
        
        _boosterPack.DOKill();
        _boosterPack.DOScale(0, 0.5f).SetEase(Ease.InBounce).OnComplete(GoKillAndSpawnCard);
        
        _isOpen = true;
    }

    private void GoKillAndSpawnCard()
    {
        // Spawn card ...
        Destroy(_boosterPack.gameObject);
    }
}
