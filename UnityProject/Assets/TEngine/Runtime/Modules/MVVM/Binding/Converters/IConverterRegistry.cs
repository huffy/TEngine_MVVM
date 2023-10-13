

using Framework.Binding.Registry;

namespace Framework.Binding.Converters
{
    public interface IConverterRegistry : IKeyValueRegistry<string, IConverter>
    {
        public const string SpriteConverterKey = "SpriteConverter";
        public const string TextureConverterKey = "TextureConverter";
    }
}
