Ext.define('GenForm.view.product.GenericNameForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.genericnameform',

    createItems: function () {
        var me = this;
        return [
            me.createHiddenField({ name:'Id'  }),
            me.createTextField({ name:'Name', fieldLabel: 'Generiek Naam', margin: '10 0 10 10' })
        ];
    },

    getGeneric: function () {
        var me = this;
        return me.getFormData();
    }

});