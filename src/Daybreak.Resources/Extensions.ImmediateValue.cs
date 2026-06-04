using ReLogic.Content;

namespace Daybreak.Resources;

partial class Extensions
{
    extension<T>(Asset<T> asset)
        where T : class
    {
        /// <summary>
        ///     Returns the value of the asset immediately, blocking if it isn't
        ///     yet loaded.
        /// </summary>
        public T ImmediateValue
        {
            get
            {
                if (asset.State == AssetState.Loading)
                {
                    asset.Wait();
                }

                return asset.Value;
            }
        }
    }
}
