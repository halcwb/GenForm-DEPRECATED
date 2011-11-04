Ext.define('GenForm.view.product.BrandNameForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.brandnameform',

    createItems: function () {
        var me = this;
        return [
            me.createHiddenField({ name:'Id'  }),
            me.createTextField({ name:'Name',   fieldLabel: 'Merk Naam', margin: '10 0 10 10' })
        ];
    },

    getBrand: function () {
        var me = this;
        return me.getFormData();
    }

});