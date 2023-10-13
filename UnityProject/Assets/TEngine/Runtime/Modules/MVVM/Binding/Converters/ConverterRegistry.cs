using Framework.Binding.Registry;
using TEngine;
using UnityEngine;

namespace Framework.Binding.Converters
{
    public class ConverterRegistry: KeyValueRegistry<string,IConverter>, IConverterRegistry
    {
        public ConverterRegistry()
        {
            this.Init();
        }

        protected virtual void Init()
        {
            Register(IConverterRegistry.SpriteConverterKey, new GenericConverter<string, Sprite>(SpriteConverter, null));
            Register(IConverterRegistry.TextureConverterKey, new GenericConverter<string, Texture>(TextureConverter, null));
        }

        private Sprite SpriteConverter(string spriteName)
        {
            var loadAsset = GameModule.Resource.LoadAssetAsyncHandle<Sprite>(spriteName);
            loadAsset.WaitForAsyncComplete();
            return loadAsset.AssetObject as Sprite;
        }
        
        private Texture TextureConverter(string textureName)
        {
            var loadAsset = GameModule.Resource.LoadAssetAsyncHandle<Texture>(textureName);
            loadAsset.WaitForAsyncComplete();
            return loadAsset.AssetObject as Texture;
        }
    }
}
