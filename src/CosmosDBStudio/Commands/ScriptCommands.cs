﻿using CosmosDBStudio.Messages;
using CosmosDBStudio.Model;
using CosmosDBStudio.Services;
using CosmosDBStudio.ViewModel;
using EssentialMVVM;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CosmosDBStudio.Commands
{
    public class ScriptCommands<TScript>
        where TScript : ICosmosScript, new()
    {
        private readonly IMessenger _messenger;
        private readonly IDialogService _dialogService;
        private readonly ICosmosAccountManager _accountManager;

        public ScriptCommands(IMessenger messenger, IDialogService dialogService, ICosmosAccountManager accountManager)
        {
            _messenger = messenger;
            _dialogService = dialogService;
            _accountManager = accountManager;
        }

        #region Open

        private DelegateCommand<ContainerScriptViewModel<TScript>>? _openCommand;
        public ICommand OpenCommand => _openCommand ??= new DelegateCommand<ContainerScriptViewModel<TScript>>(Open);

        private void Open(ContainerScriptViewModel<TScript> scriptVm)
        {
            _messenger.Publish(new OpenScriptMessage<TScript>(
                scriptVm.Container.Database.Account.Id,
                scriptVm.Container.Database.Id,
                scriptVm.Container.Id,
                scriptVm.Script));
        }

        #endregion

        #region

        private DelegateCommand<ContainerScriptFolderViewModel>? _createCommand;
        public ICommand CreateCommand => _createCommand ??= new DelegateCommand<ContainerScriptFolderViewModel>(Create);

        private void Create(ContainerScriptFolderViewModel parent)
        {
            var result = _dialogService.TextPrompt("Enter id for new item");
            if (!result.TryGetValue(out var id))
            {
                return;
            }

            if (parent.Children.OfType<ContainerScriptViewModel<TScript>>().Any(c => c.Script.Id == id))
            {
                _dialogService.ShowError("Another item with the same name already exists");
                return;
            }

            var script = new TScript();
            script.Id = id;
            script.Body = $@"function {id} () {{
    
}}";

            var container = parent.Container;
            _messenger.Publish(new OpenScriptMessage<TScript>(
                container.Database.Account.Id,
                container.Database.Id,
                container.Id,
                script));
        }

        #endregion

        #region Delete

        private AsyncDelegateCommand<ContainerScriptViewModel<TScript>>? _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ??= new AsyncDelegateCommand<ContainerScriptViewModel<TScript>>(DeleteAsync);

        private async Task DeleteAsync(ContainerScriptViewModel<TScript> scriptVm)
        {
            if (!_dialogService.Confirm($"Are you sure you want to delete {scriptVm.Description} '{scriptVm.Script.Id}'?"))
                return;

            await scriptVm.DeleteAsync(_accountManager);

            scriptVm.Parent?.ReloadChildren();
        }

        #endregion
    }
}