using System;
//
namespace AssemblyCSharp{
	
	public interface StrategyFactory {
		OutputStrategy OutputStrategy();
		Inputstrategy InputStrategy();
	}
}

