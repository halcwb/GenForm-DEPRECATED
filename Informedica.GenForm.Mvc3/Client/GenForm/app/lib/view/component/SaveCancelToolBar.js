/**
 * Created by .
 * User: hal
 * Date: 26-4-11
 * Time: 10:43
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.lib.view.component.SaveCancelToolbar', {
    extend: 'Ext.toolbar.Toolbar',
    alias: 'widget.savecanceltoolbar',

    items: [
            { text: 'Opslaan', action: 'save'},
            { text: 'Cancel', action: 'cancel'}
    ]
    
});