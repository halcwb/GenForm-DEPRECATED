Ext.define('GenForm.lib.util.SaveItemHandler', {

    onSaved: function (windowname, title, result, addToStore) {
        var me = this,
            window = Ext.ComponentQuery.query(windowname)[0];

        if (result.data.success) {
            Ext.MessageBox.alert(title, result.data.Name);
            addToStore(result.data);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Item could not be saved: ', result.message);
        }
    },

    onSave: function (me, directFn, callBackFn, item) {
        directFn(item.data, {scope: me, callback: callBackFn});
    }

});