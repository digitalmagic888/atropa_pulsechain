// SPDX-License-Identifier: Sharia
pragma solidity ^0.8.25;
import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "@openzeppelin/contracts/token/ERC20/extensions/ERC20Burnable.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "addresses.sol";
import "articleregistry.sol";
import "asset.sol";
import "whitelist.sol";

abstract contract Incorporation is ERC20, ERC20Burnable, Ownable, Asset, ArticleRegistry {   
    uint256 immutable internal maxSupply;
    uint256 internal minDivisor = 1110;
    IncorporationType immutable internal AssetClass;
    address immutable internal TreasuryReceiver;

    function(uint256, IncorporationType) internal returns (bool) Disbersement;

    function SyncableAssetClass(IncorporationType class) public pure returns (bool) {
        return (class != IncorporationType.EXCHANGE && class != IncorporationType.FUTURE && class != IncorporationType.CAP && class != IncorporationType.CLIMA);
    }

    function Register(address pool, uint256 divisor, address registree, uint256 length, IncorporationType class) public override {
        assert(length < 367);
        AssertAccess(msg.sender);
        if(class != IncorporationType.FUTURE && class != IncorporationType.CAP)
            assert(divisor > minDivisor);
        if(SyncableAssetClass(class)) assert(Asset.Sync(pool) == true);
        SetArticle(pool, divisor, registree, length * 1 days, class);
    }

    function MintCAP(uint256 amount) public {
        assert(Articles[msg.sender].Class == IncorporationType.CLIMA);
        if(totalSupply() + amount < maxSupply)
            _mint(TreasuryReceiver, amount);
        else
            Disbersement(amount, IncorporationType.OFFSET);
    }

    function transfer(address to, uint256 amount) public override returns (bool) {
        address owner = _msgSender();
        if(!(AssetClass == IncorporationType.SUBSIDY))
            if(Registry.Contains(to) || Registry.Contains(owner))
                Disbersement(amount, IncorporationType.COMMODITY);
        if(Registry.Contains(to) && Registry.Contains(owner) && (IsClass(owner, IncorporationType.EXCHANGE)))
                Disbersement(amount, IncorporationType.OPTION);
        if(Registry.Contains(to) && Registry.Contains(owner) && (IsClass(to, IncorporationType.EXCHANGE)))
                Disbersement(amount, IncorporationType.FUTURE);
        _transfer(owner, to, amount);
        return true;
    }

    function transferFrom(address from, address to, uint256 amount) public override returns (bool) {
        address spender = _msgSender();
        if(!(AssetClass == IncorporationType.HEDGE))
            if(Registry.Contains(from) || Registry.Contains(to))
                Disbersement(amount, IncorporationType.COMMODITY);
        if(Registry.Contains(from) && Registry.Contains(to) && (IsClass(from, IncorporationType.EXCHANGE)))
            Disbersement(amount, IncorporationType.OPTION);
        if(Registry.Contains(from) && Registry.Contains(to) && (IsClass(to, IncorporationType.EXCHANGE)))
            Disbersement(amount, IncorporationType.FUTURE);
        _spendAllowance(from, spender, amount);
        _transfer(from, to, amount);
        return true;
    }
}