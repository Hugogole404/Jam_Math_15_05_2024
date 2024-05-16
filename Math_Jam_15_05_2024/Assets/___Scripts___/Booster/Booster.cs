using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Booster : MonoBehaviour
{
    [Header("--- Setup ---")] [SerializeField]
    private Transform _boosterPack; // Référence au transform du booster pack
    [SerializeField] private int _touchNeededToOpen;

    [SerializeField] private GameObject _cardNumberPrefab; // Matériau du booster pack
    [SerializeField] private GameObject _cardOpePrefab; // Matériau du booster pack
    [SerializeField] private GameObject _cardMathematicianPrefab; // Matériau du booster pack

    [Header("--- Timings Hit ---")] [SerializeField]
    private float _bounceHeight = 0.5f; // Hauteur du rebond

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
    private BoosterScriptable _boosterInfos;

    void Start()
    {
        // Sauvegarder la taille initiale et la rotation initiale du booster pack
        _startScale = _boosterPack.localScale;
        _startRotation = _boosterPack.localRotation;
        // OnJumpAnim();
    }

    public void Init(BoosterScriptable boosterInfo, Vector3 jumpPos)
    {
        _boosterInfos = boosterInfo;
        OnJumpAnim(jumpPos);
    }

    private void OnJumpAnim(Vector3 jumpPos)
    {
        var pos = gameObject.transform.position;
        gameObject.transform.DOJump(new Vector3(pos.x + jumpPos.x, pos.y + jumpPos.y, pos.z + jumpPos.z), 2, 1, 1f).SetEase(Ease.Linear);
    }

    private void OnMouseDown()
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
        _boosterPack.DOPunchScale(new Vector3(_punchIntensity, _punchIntensity, _punchIntensity), _scaleDuration,
                _vibrato)
            .SetEase(Ease.OutQuad) // Easing pour un effet plus naturel
            .OnComplete(() =>
            {
                // Animation de rotation gauche droite
                _boosterPack.DOLocalRotateQuaternion(_startRotation * Quaternion.Euler(0, 0, _rotationAngle),
                        _rotationDuration / 2f)
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
        Renderer renderer = _boosterPack.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material boosterMaterial = renderer.material;
            Color originalColor = boosterMaterial.color;
            boosterMaterial.DOColor(_hitColor, _hitDuration)
                .SetEase(Ease.OutQuad) // Easing pour un effet plus naturel
                .OnComplete(() =>
                {
                    boosterMaterial.color = originalColor; // Rétablir la couleur d'origine
                });
            // if (_boosterMaterial != null)
            // {
            //     Color originalColor = _boosterMaterial.color;
            //     _boosterMaterial.DOColor(_hitColor, _hitDuration)
            //         .SetEase(Ease.OutQuad) // Easing pour un effet plus naturel
            //         .OnComplete(() =>
            //         {
            //             _boosterMaterial.color = originalColor; // Rétablir la couleur d'origine
            //         });
            // }
        }
    }

    void OnDeathAnim()
    {
        if (_isOpen) return;

        _boosterPack.DOKill();
        _boosterPack.DOScale(0, 0.5f).SetEase(Ease.InBounce).OnComplete(GoKillAndSpawnCard);

        // Spawn card ...
        SpawnNumbers(_boosterInfos.EvenNumbers, _boosterInfos.EvenNumbersCount);
        SpawnNumbers(_boosterInfos.OddNumbers, _boosterInfos.OddNumbersCount);
        SpawnOperators();
        SpawnMathematicians();
        print("spawn");
        
        _isOpen = true;
    }

    private void GoKillAndSpawnCard()
    {
        
        
        Destroy(_boosterPack.gameObject);
    }
    
    void SpawnNumbers(List<int> numberList, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, numberList.Count);
            int randomNumber = numberList[randomIndex];
            
            GameObject go = Instantiate(_cardNumberPrefab, transform.position, _cardNumberPrefab.transform.rotation);
            go.GetComponent<CardNumber>().Init(randomNumber, true);
        }
    }

    void SpawnOperators()
    {
        // Spawner des opérateurs
        for (int i = 0; i < _boosterInfos.OperatorsCount; i++)
        {
            int randomIndex = Random.Range(0, _boosterInfos.Operators.Count);
            Operators randomOperator = _boosterInfos.Operators[randomIndex];
            // Debug.Log("Spawned operator: " + randomOperator);
            
            GameObject go = Instantiate(_cardOpePrefab, transform.position, _cardOpePrefab.transform.rotation);
            go.GetComponent<CardOperator>().Init(randomOperator);
        }
    }

    void SpawnMathematicians()
    {
        // Spawner des mathématiciens
        for (int i = 0; i < _boosterInfos.MathematiciansCount; i++)
        {
            // Spawn d'un mathématicien
            GameObject go = Instantiate(_cardMathematicianPrefab, transform.position, _cardMathematicianPrefab.transform.rotation);
            // go.GetComponent<CardMathChara>().Init(randomNumber, true);
            // Debug.Log("Spawned mathematician");
        }
    }

}