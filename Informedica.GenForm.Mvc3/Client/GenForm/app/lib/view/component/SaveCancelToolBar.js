Ext.define('GenForm.lib.view.component.SaveCancelToolbar', {
    extend: 'Ext.toolbar.Toolbar',
    alias: 'widget.savecanceltoolbar',

    items: [
            { text: 'Opslaan', action: 'save'},
            { text: 'Cancel', action: 'cancel'}
    ]
    
});