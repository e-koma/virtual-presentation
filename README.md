# virtual-presentation
- Unity 2020.3.6f1

# Note
- ※ M1 Mac 以降で build時に`DllNotFoundException: StandaloneFileBrowser` が出るが Intel 64-bit buildでひとまず回避できる
  - https://github.com/gkngkc/UnityStandaloneFileBrowser/issues/109

# GitLFS同期
```
git config -f .lfsconfig lfs.url (個人AWSのLFS URL)
```
.lfsをダウンロード
起動時にエラーがでたらPacage Magagerなどから不要アセット削除

# Assets

- Models
```
Assets/MyAssets/Models
├── komai_san.fbx
├── komai_san_suit.fbx
├── yuto_suits.png
├── yuto_suits_mat.mat
├── yuto_t_shirts_hair_fix02.png
└── yuto_t_shrts_mat.mat
```

- Motions
```
Assets/MyAssets/Motions/
├── conference_briefing01
│   ├── conference_briefing_m_270770.fbx
│   └── conference_briefing_m_270770.json
├── conference_briefing02_zero_root_animation
│   ├── conference_briefing_m_270770.fbx
│   └── conference_briefing_m_270770.json
├── standing_discuss01
│   ├── standing_discuss_m_270744.fbx
│   └── standing_discuss_m_270744.json
├── standing_discuss02_zero_root_animation
│   ├── standing_discuss_m_270744.fbx
│   └── standing_discuss_m_270744.json
├── greet
│   ├── move_greet_m.fbx
│   ├── move_greet_m.json
```

- Parameter
  - U Lip Sync
    - Output Sound Gain: 0
  - U Lip Sync Microphonne
    - Is Auto Start: true

- [Sounds](http://www.soundgator.com/)

# Project Settings
- Player -> Other Settings -> Active Input Handling -> Both (新旧Input Systemを有効化)
- Player -> Other Settings -> Configuration -> Api Compatibility Level -> .NET 4.x

# Build Prepare
- Project Settings -> Player -> iOS -> Microphone Usage Description -> 適当な文字列入力
- Project Settings -> Player -> Configuration -> Microphone Usage Descriptiion(入力) -> Apply
