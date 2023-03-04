using UnityEngine;

/// <summary>
/// 爆発エフェクトを制御するコンポーネント
/// </summary>
public class ExplosionController : MonoBehaviour
{
    ParticleSystem[] _particleSystems = default;
    AudioSource _audio = default;

    void Start()
    {
        _particleSystems = this.transform.GetComponentsInChildren<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        Explode(Vector3.up * -100f, 0f); // 見えない所で一回爆発させて初期化する
    }

    /// <summary>
    /// 指定した位置で爆発させる
    /// </summary>
    /// <param name="position">爆発する座標</param>
    /// <param name="volume">爆発音の音量</param>
    public void Explode(Vector3 position, float volume = 1f)
    {
        this.transform.position = position;

        foreach (var p in _particleSystems)
        {
            p.Play();
        }

        if (_audio)
        {
            _audio.volume = volume;
            _audio.Play();
        }
    }
}
