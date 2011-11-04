Ext.define('GenForm.view.product.ProductSubstanceForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.productsubstanceform',

    title:'Artikel Stof',

    createItems: function () {
        var me = this;
        return [
            {
                defaultType: 'textfield',
                padding: '10x',

                items: [
                    me.createOrderNumberField(),
                    me.createSubstanceCombo(),
                    me.createQuantityField(),
                    me.createUnitCombo()
                ]
            }
        ];
    },

    createQuantityField: function () {
        var me = this, config;

        config = {
            fieldLabel: 'Hoeveelheid',
            margin: '10 10 10 10',
            name: 'Quantity'
        };
        return me.createNumberField(config);
    },

    createOrderNumberField: function () {
        var me = this, config;

        config = {
            fieldLabel: 'Volgorde',
            name: 'OrderNumber',
            margin: '10 10 10 10',
            allowBlank:true
        };

        return me.createNumberField(config);
    },

    createSubstanceCombo: function () {
        var me = this, config;

        config = {
            xtype: 'comboboxcontainer',
            editAction: 'editSubstance',
            addAction: 'addSubstance',
            margin: '10 10 10 10',
            comboBox: me.createComboBox({
                idName: true,
                itemId: 'cboSubstance',
                fieldLabel: 'Stof naam',
                name: 'Substance',
                hideEmptyLabel: true,
                flex: 1,
                directFn: GenForm.server.UnitTest.GetSubstanceNames
            })
        }

        return config;
    },

    createUnitCombo: function () {
        var me = this, config;

        config = {
            xtype: 'comboboxcontainer',
            editAction: 'editSubstanceUnit',
            addAction: 'addSubstanceUnit',
            margin: '10 10 10 10',
            comboBox: me.createComboBox({
                idName: true,
                itemId: 'cboSubstanceUnit',
                fieldLabel: 'Eenheid',
                name: 'Unit',
                hideEmptyLabel: true,
                flex: 1,
                directFn: GenForm.server.UnitTest.GetUnitNames
            })
        };

        return config;
    }

});