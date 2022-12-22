using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Features.Command
{
    public class ImageRequestCommand : BaseCommand
    {
        private const string ImageUrl = "https://picsum.photos/256";
        private readonly CancellationToken _cancellationToken;
        public ImageRequestCommand(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }       

        public override async UniTask<CommandResult> Do()
        {
            var rez = new CommandResult();
            rez.Body = await DownloadImageAsync(_cancellationToken);
            rez.Status = rez.Body != null ? CommandStatus.Success : CommandStatus.Failed;
            return rez;
        }        

        public async UniTask<Texture2D> DownloadImageAsync(CancellationToken cancellationToken = default)
        {
            using var www = UnityWebRequestTexture.GetTexture(ImageUrl);

            await www.SendWebRequest().WithCancellation(cancellationToken);

            return www.result == UnityWebRequest.Result.Success ? DownloadHandlerTexture.GetContent(www) : null;
        }
    }
}