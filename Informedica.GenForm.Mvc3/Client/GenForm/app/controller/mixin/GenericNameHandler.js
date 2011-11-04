Ext.define('GenForm.controller.mixin.GenericNameHandler', {

    addGenericToStore: function (generic) {
        var me = this,
            store = me.getGenericStore();

        store.add(generic);
    },

    createEmptyGeneric: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.common.IdName');
    },

    createGenericWindow: function () {
        return Ext.create('GenForm.view.product.GenericNameWindow', { width: 400, height: 200 });
    },

    onAddGenericName: function () {
        var me = this,
            window = me.getGenericWindow(me.createEmptyGeneric()).show();
        window.setTitle('Nieuwe generiek naam');
        window.show();
    },

    onEditGenericName: function (button) {
        var me = this,
            form = button.findParentByType('productform'),
            generic = form.fields.GenericName.findRecord('Name', form.fields.GenericName.getValue()),
            window = me.getGenericWindow(generic);

        window.setTitle('Bewerk generiek naam: ' + generic.data.Name);
        window.show();
    },

    getGeneric: function (button) {
        return button.findParentByType('genericnamewindow').forms.GenericNameForm.getGeneric();
    },

    getGenericWindow: function (generic) {
        var me = this, window;

        window = me.createGenericWindow();
        window.loadWithGeneric(generic);
        return window;
    },

    onGenericSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('genericnamewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Generic saved: ', result.data.Name);
            me.addGenericToStore(result.data);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Generic could not be saved: ', result.message);
        }
    },

    saveGeneric: function (button) {
        var me = this,
            generic = me.getGeneric(button);

        GenForm.server.UnitTest.AddNewGenericName(generic.data, {scope: me, callback:me.onGenericSaved});
    },

    getGenericStore: function () {
        var me = this, window = Ext.ComponentQuery.query('productwindow')[0];
        return window.getProductForm().fields.GenericName.store;
    }

});